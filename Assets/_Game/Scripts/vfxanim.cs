using UnityEngine;

public class LaunchVFXWithAnim : MonoBehaviour
{
    [Header("VFX Settings")]
    public GameObject vfxPrefab;
    public float spawnDistance = 2f;
    public float vfxLifetime = 2f;
    public float vfxDelay = 1f; // <- délai avant lancement du VFX

    [Header("Animation")]
    public Animator animator;
    public string triggerName = "attacktest";

    [Header("Movement Control")]
    public MonoBehaviour movementScript; // Ton script de mouvement (à drag)

    private bool isFreezing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isFreezing)
        {
            PlayAttackAnimation();
            StartCoroutine(FreezeAndLaunchVFX());
        }
    }

    void PlayAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(triggerName);
        }
    }

    System.Collections.IEnumerator FreezeAndLaunchVFX()
    {
        isFreezing = true;

        if (movementScript != null)
            movementScript.enabled = false;

        // Attendre 1 seconde avant de lancer le VFX
        yield return new WaitForSeconds(vfxDelay);

        LaunchVFX();

        // Réactiver le mouvement après la même durée
        if (movementScript != null)
            movementScript.enabled = true;

        isFreezing = false;
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
