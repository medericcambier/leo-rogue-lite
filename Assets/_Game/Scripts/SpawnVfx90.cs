using UnityEngine;

public class LaunchGroundVFX : MonoBehaviour
{
    [Header("VFX Settings")]
    public GameObject vfxPrefab;
    public float distanceInFront = 2f;
    public float vfxLifetime = 2f;
    public float vfxSpeed = 2f;
    public float sideAngleY = 90f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LaunchVFXOnGround();
        }
    }

    void LaunchVFXOnGround()
    {
        if (vfxPrefab == null)
        {
            Debug.LogWarning("Aucun VFX assigné !");
            return;
        }

        Vector3 forwardFlat = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;

        
        Vector3 startPosition = transform.position + forwardFlat * distanceInFront + Vector3.up * 1.5f; // hauteur de tir

        RaycastHit hit;
        Vector3 spawnPosition;

        
        if (Physics.Raycast(startPosition, Vector3.down, out hit, 10f))
        {
            spawnPosition = hit.point;
        }
        else
        {
            
            spawnPosition = new Vector3(startPosition.x, 0f, startPosition.z);
        }

        
        Quaternion baseRotation = Quaternion.LookRotation(forwardFlat);
        Quaternion vfxRotation = baseRotation * Quaternion.Euler(0, sideAngleY, 0);

       
        GameObject vfxInstance = Instantiate(vfxPrefab, spawnPosition, vfxRotation);

       
        Rigidbody rb = vfxInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = vfxInstance.transform.forward * vfxSpeed;
        }

        if (vfxLifetime > 0f)
        {
            Destroy(vfxInstance, vfxLifetime);
        }
    }
}
