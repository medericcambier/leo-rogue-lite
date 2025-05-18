using UnityEngine;

public class SpawnVFX : MonoBehaviour
{
    [Header("VFX Settings")]
    public GameObject vfxPrefab;        
    public float distanceInFront = 2f;  
    public float vfxLifetime = 2f;       

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnEffectInLine();
        }
    }

    void SpawnEffectInLine()
    {
        if (vfxPrefab == null)
        {
            Debug.LogWarning("Aucun VFX assigné !");
            return;
        }

       
        Vector3 forwardFlat = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 spawnPosition = transform.position + forwardFlat * distanceInFront;
        Quaternion spawnRotation = Quaternion.LookRotation(forwardFlat);

        
        GameObject vfxInstance = Instantiate(vfxPrefab, spawnPosition, spawnRotation);

        if (vfxLifetime > 0f)
        {
            Destroy(vfxInstance, vfxLifetime);
        }
    }
}


