using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100f; 
    public float gravity = 9.81f; 
    public float jumpHeight = 2f; 
    public Transform cameraTransform;
    public bool canMove = true; 


    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private Vector3 cameraOffset = new Vector3(-5f, 8f, -5f);

    void Start()
    {
        controller = GetComponent<CharacterController>();

        
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + cameraOffset;
            cameraTransform.rotation = Quaternion.Euler(30f, 45f, 0f); 
        }
    }

    void Update()
    {


        if (!canMove) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.Translate(movement * speed * Time.deltaTime);


       // isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) { moveZ = 1f; moveX = 1f; };
        if (Input.GetKey(KeyCode.S)) { moveZ = -1f; moveX = -1f; };
        if (Input.GetKey(KeyCode.A)) { moveX = -1f; moveZ = 1f; };
        if (Input.GetKey(KeyCode.D)) { moveX = 1f; moveZ = -1f; };

       
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        //controller.Move(move * speed * Time.deltaTime);

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;
       // controller.Move(velocity * Time.deltaTime);

        
        if (cameraTransform != null)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, transform.position + cameraOffset, 0.1f);
        }
    }
}
