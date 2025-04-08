using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;             // Ton joueur
    public Vector3 offset = new Vector3(0f, 3f, -6f); // Position relative à ton joueur
    public float followSpeed = 5f;       // Suivi smooth
    public float rotateSpeed = 5f;       // Vitesse de rotation avec la souris

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    public bool allowMouseRotation = true;

    void LateUpdate()
    {
        if (target == null) return;

        // Rotation à la souris (optionnel)
        if (allowMouseRotation)
        {
            currentRotationX += Input.GetAxis("Mouse X") * rotateSpeed;
            currentRotationY -= Input.GetAxis("Mouse Y") * rotateSpeed;
            currentRotationY = Mathf.Clamp(currentRotationY, -20f, 60f);
        }

        // Calcul de la position derrière le joueur
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f); // Regarde vers le haut du joueur
    }
}
