using UnityEngine;
using System.Collections;

public class VFXVariant2 : MonoBehaviour
{
    public GameObject vfxPrefab2;
    public Transform spawnPoint;
    public PlayerMovement playerMovement;
    public float vfxDuration = 8f;
    public Camera mainCamera;
    private bool isSpawning = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isSpawning) 
        {
            StartCoroutine(SpawnEffect());
        }
    }

    IEnumerator SpawnEffect()
    {
        if (vfxPrefab2 == null || spawnPoint == null || playerMovement == null || mainCamera == null)
        {
            Debug.LogWarning("Assignez tous les éléments nécessaires dans l'Inspector !");
            yield break;
        }

        isSpawning = true;
        playerMovement.canMove = false;

        Plane plane = new Plane(Vector3.up, spawnPoint.position);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint = spawnPoint.position;

        if (plane.Raycast(ray, out float distance))
        {
            targetPoint = ray.GetPoint(distance);
        }

        Vector3 direction = (targetPoint - spawnPoint.position).normalized;
        Quaternion vfxRotation = Quaternion.identity;

        if (direction != Vector3.zero)
            vfxRotation = Quaternion.LookRotation(direction);

        GameObject vfxInstance = Instantiate(vfxPrefab2, spawnPoint.position, vfxRotation);

        Destroy(vfxInstance, vfxDuration);
        yield return new WaitForSeconds(vfxDuration);

        playerMovement.canMove = true;
        isSpawning = false;
    }
}
