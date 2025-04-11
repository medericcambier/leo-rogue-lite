using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(-2f, 3f, -6f); // D�cal� sur la gauche pour mieux voir devant
    public float followSpeed = 5f;
    public float rotateSpeed = 5f;

    private float currentRotationX = 0f;
    private float currentRotationY = 10f; // L�g�re inclinaison vers le bas au d�part

    public bool allowMouseRotation = true;

    void LateUpdate()
    {
        if (target == null) return;

        // Rotation � la souris (optionnel)
        if (allowMouseRotation)
        {
            currentRotationX += Input.GetAxis("Mouse X") * rotateSpeed;
            currentRotationY -= Input.GetAxis("Mouse Y") * rotateSpeed;
            currentRotationY = Mathf.Clamp(currentRotationY, -10f, 45f); // Angle vertical pas trop extr�me
        }

        // Calcul de la rotation et position
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // D�cale le point que la cam�ra regarde : un peu devant le joueur
        Vector3 lookTarget = target.position + target.forward * 4f + Vector3.up * 1.5f;
        transform.LookAt(lookTarget);
    }
}
