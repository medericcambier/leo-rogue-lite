using UnityEngine;

public class VFXOnKeyE : MonoBehaviour
{
    public GameObject vfxPrefab;
    public float spawnDistance = 2f;
    public float vfxLifetime = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LaunchVFX();
        }
    }

    void LaunchVFX()
    {
        if (vfxPrefab == null) return;

        Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 spawnPos = transform.position + forward * spawnDistance;

        GameObject vfx = Instantiate(vfxPrefab, spawnPos, Quaternion.LookRotation(forward));
        Destroy(vfx, vfxLifetime);
    }
}
