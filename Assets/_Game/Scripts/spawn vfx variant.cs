using UnityEngine;
using System.Collections;

public class SpawnVFXVariant : MonoBehaviour
{
    public GameObject vfxPrefab;
    public Transform spawnPoint;
    public PlayerMovement playerMovement;
    public float vfxDuration = 8f;
    public Camera mainCamera;

    public Animator animator;
    public string attackTrigger = "SlashTrigger";

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


            // V�rifie si l'objet instanci� a bien un Animator
            Animator vfxAnimator = vfxInstance.GetComponent<Animator>();
            if (vfxAnimator != null)
            {
                // V�rifie si l'Animator a bien un Controller assign�
                if (vfxAnimator.runtimeAnimatorController != null)
                {
                    // D�clenche l'animation du slash
                    vfxAnimator.SetTrigger("SlashAttack");
                }
                else
                {
                    Debug.LogError("L'Animator n'a pas de RuntimeAnimatorController !");
                }
            }
            else
            {
                Debug.LogError("Le prefab instanci� ne poss�de pas d'Animator !");
            }

            yield return new WaitForSeconds(vfxDuration);

            playerMovement.canMove = true;

            // D�truit le VFX apr�s la dur�e d�finie
            Destroy(vfxInstance, 0.5f);
        }
        else
        {
            Debug.LogWarning("Assignez tous les �l�ments n�cessaires dans l'Inspector !");
        }
    }

}
