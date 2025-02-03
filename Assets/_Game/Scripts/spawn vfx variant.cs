using UnityEngine;
using System.Collections;

public class SpawnVFXVariant : MonoBehaviour
{
    public GameObject vfxPrefab;
    public Transform spawnPoint;
    public PlayerMovement playerMovement;
    public float vfxDuration = 8f;
    public Camera mainCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Appuyer sur Q pour déclencher le VFX
        {
            StartCoroutine(SpawnEffect());
        }
    }

    IEnumerator SpawnEffect()
    {
        if (vfxPrefab != null && spawnPoint != null && playerMovement != null && mainCamera != null)
        {
            playerMovement.canMove = false;

            // Définir un plan horizontal à la hauteur du spawnPoint
            Plane plane = new Plane(Vector3.up, spawnPoint.position);

            // Raycast depuis la caméra vers la position de la souris
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint = spawnPoint.position; // Valeur par défaut

            float distance;
            if (plane.Raycast(ray, out distance)) // Si le raycast touche le plan
            {
                targetPoint = ray.GetPoint(distance); // Récupérer le point d'intersection
            }

            // Calculer la direction vers la cible
            Vector3 direction = (targetPoint - spawnPoint.position).normalized;

            // Créer une rotation qui regarde vers la cible
            Quaternion vfxRotation = Quaternion.LookRotation(direction);

            // Instancier le VFX avec la rotation correcte
            GameObject vfxInstance = Instantiate(vfxPrefab, spawnPoint.position, vfxRotation);

            yield return new WaitForSeconds(vfxDuration);

            playerMovement.canMove = true;
        }
        else
        {
            Debug.LogWarning("Assignez tous les éléments nécessaires dans l'Inspector !");
        }
    }
}
