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
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            StartCoroutine(SpawnEffect());
        }
    }

    IEnumerator SpawnEffect()
    {
        if (vfxPrefab != null && spawnPoint != null && playerMovement != null && mainCamera != null)
        {
            playerMovement.canMove = false;

            Plane plane = new Plane(Vector3.up, spawnPoint.position);

            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint = spawnPoint.position; 

            float distance;
            if (plane.Raycast(ray, out distance)) 
            {
                targetPoint = ray.GetPoint(distance); 
            }

           
            Vector3 direction = (targetPoint - spawnPoint.position).normalized;


            Quaternion vfxRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0);


            GameObject vfxInstance = Instantiate(vfxPrefab, spawnPoint.position, vfxRotation);

            yield return new WaitForSeconds(vfxDuration);

            playerMovement.canMove = true;

            // Détruit le VFX après la durée définie
            Destroy(vfxInstance, 0.5f);
        }
        else
        {
            Debug.LogWarning("Assignez tous les éléments nécessaires dans l'Inspector !");
        }
    }
}
