using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Le joueur
    public Vector3 offset = new Vector3(0f, 10f, -10f); // Position relative
    public float smoothSpeed = 5f; // Vitesse de suivi

    void LateUpdate()
    {
        if (target == null) return;

        // Position cible
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        // Regarde toujours le joueur (de haut en biais)
        transform.LookAt(target.position + Vector3.up * 1.5f); // Ajuste le regard au niveau du torse
    }
}
