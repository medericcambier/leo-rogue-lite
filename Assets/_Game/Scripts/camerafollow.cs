using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(-2f, 3f, -6f); // Décalé sur la gauche pour mieux voir devant
    public float followSpeed = 5f;
    public float rotateSpeed = 5f;

    private float currentRotationX = 0f;
    private float currentRotationY = 10f; // Légère inclinaison vers le bas au départ

    public bool allowMouseRotation = true;

    void LateUpdate()
    {
        if (target == null) return;

        // Rotation à la souris (optionnel)
        if (allowMouseRotation)
        {
            currentRotationX += Input.GetAxis("Mouse X") * rotateSpeed;
            currentRotationY -= Input.GetAxis("Mouse Y") * rotateSpeed;
            currentRotationY = Mathf.Clamp(currentRotationY, -10f, 45f); // Angle vertical pas trop extrême
        }

        // Calcul de la rotation et position
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Décale le point que la caméra regarde : un peu devant le joueur
        Vector3 lookTarget = target.position + target.forward * 4f + Vector3.up * 1.5f;
        transform.LookAt(lookTarget);
    }
}
