using UnityEngine;
using System.Collections; 

public class SpawnVFX : MonoBehaviour
{
    public GameObject vfxPrefab; 
    public Transform spawnPoint; 
    public PlayerMovement playerMovement; 
    public float vfxDuration = 8f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SpawnEffect()); 
        }
    }

    IEnumerator SpawnEffect()
    {
        if (vfxPrefab != null && spawnPoint != null && playerMovement != null)
        {
            playerMovement.canMove = false;

            GameObject vfxInstance = Instantiate(vfxPrefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(-90, 0, 0));

            yield return new WaitForSeconds(vfxDuration); 

            playerMovement.canMove = true; 
        }
        else
        {
            Debug.LogWarning("Assurez-vous que le VFX et PlayerMovement sont assignés !");
        }
    }
}
