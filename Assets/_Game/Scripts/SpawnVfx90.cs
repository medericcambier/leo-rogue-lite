using UnityEngine;

public class LaunchSideVFX_Slow : MonoBehaviour
{
    [Header("VFX Settings")]
    public GameObject vfxPrefab;
    public float distanceInFront = 2f;
    public float vfxLifetime = 2f;
    public float vfxSpeed = 1f; 

    [Tooltip("Angle en Y pour orienter le VFX (90 = droite, -90 = gauche)")]
    public float sideAngleY = 90f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnMovingVFX();
        }
    }

    void SpawnMovingVFX()
    {
        if (vfxPrefab == null) return;

        Vector3 forwardFlat = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 spawnPosition = transform.position + forwardFlat * distanceInFront;

        Quaternion baseRotation = Quaternion.LookRotation(forwardFlat);
        Quaternion vfxRotation = baseRotation * Quaternion.Euler(0, sideAngleY, 0);

        GameObject vfxInstance = Instantiate(vfxPrefab, spawnPosition, vfxRotation);

       
        vfxInstance.AddComponent<SimpleForwardMover>().speed = vfxSpeed;

        if (vfxLifetime > 0f)
        {
            Destroy(vfxInstance, vfxLifetime);
        }
    }
}

// ?? Script de déplacement lent
public class SimpleForwardMover : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
