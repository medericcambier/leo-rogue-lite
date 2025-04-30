using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public Transform cameraPivot; // L'empty qui contient la caméra

    private Rigidbody rb;
    private float rotationY = 0f;
    private Vector3 currentVelocity = Vector3.zero;
    public float acceleration = 10f;

    // Gravité et Saut
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private bool isGrounded;

    private Animator animator;
    private bool IsBlocking = false;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Empêche le Rigidbody de tourner (on gère la rotation manuellement)
        Cursor.lockState = CursorLockMode.Locked; // Cache le curseur et le centre
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Rotation horizontale avec la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY += mouseX;
        Quaternion targetRotation = Quaternion.Euler(0f, rotationY, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // rotation fluide

        // Vérification si le joueur est au sol
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f); // Utilise un Raycast pour détecter le sol

        // Appliquer la gravité
        if (!isGrounded)
        {
            rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration); // Applique la gravité
        }
        else
        {
            // Réinitialise la vitesse verticale (si on est au sol)
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Réinitialiser la vitesse verticale
            }
        }

        // Déplacement en avant/arrière/gauche/droite selon l'orientation du personnage
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float strafeFactor = 0.5f; // ← réduis à 50% par exemple
        Vector3 move = transform.right * h * strafeFactor + transform.forward * v;

        currentVelocity = Vector3.Lerp(currentVelocity, move * moveSpeed, acceleration * Time.deltaTime);

        // Applique le mouvement avec le Rigidbody
        rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z); // Garder la vitesse verticale intacte

        // Saut (si le joueur est au sol)
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse); // Applique une impulsion pour le saut
        }



        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
        animator.SetFloat("ForwardSpeed", localVel.z);
        animator.SetFloat("StrafeSpeed", localVel.x);

        if (Input.GetMouseButtonDown(1))
        {
            IsBlocking = true;
            animator.SetBool("IsBlocking", IsBlocking);
        }

        if (Input.GetMouseButtonUp(1)) 
        {
            IsBlocking = false;
            animator.SetBool("IsBlocking", IsBlocking);
        }


    }
}




