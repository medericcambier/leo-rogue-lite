using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public Transform cameraPivot; // L'empty qui contient la cam�ra

    private Rigidbody rb;
    private float rotationY = 0f;
    private Vector3 currentVelocity = Vector3.zero;
    public float acceleration = 10f;

    // Gravit� et Saut
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Emp�che le Rigidbody de tourner (on g�re la rotation manuellement)
        Cursor.lockState = CursorLockMode.Locked; // Cache le curseur et le centre
    }

    void Update()
    {
        // Rotation horizontale avec la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY += mouseX;
        Quaternion targetRotation = Quaternion.Euler(0f, rotationY, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // rotation fluide

        // V�rification si le joueur est au sol
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f); // Utilise un Raycast pour d�tecter le sol

        // Appliquer la gravit�
        if (!isGrounded)
        {
            rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration); // Applique la gravit�
        }
        else
        {
            // R�initialise la vitesse verticale (si on est au sol)
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // R�initialiser la vitesse verticale
            }
        }

        // D�placement en avant/arri�re/gauche/droite selon l'orientation du personnage
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        currentVelocity = Vector3.Lerp(currentVelocity, move * moveSpeed, acceleration * Time.deltaTime);

        // Applique le mouvement avec le Rigidbody
        rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z); // Garder la vitesse verticale intacte

        // Saut (si le joueur est au sol)
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse); // Applique une impulsion pour le saut
        }
    }
}




