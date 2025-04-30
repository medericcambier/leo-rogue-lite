using UnityEngine;

public class SimpleTeleport : MonoBehaviour
{
    public Transform teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("TeleportTrigger"))
        {
            if (teleportTarget != null)
            {
                Debug.Log(teleportTarget);
                transform.position = teleportTarget.position;
            }
        }
    }
}
