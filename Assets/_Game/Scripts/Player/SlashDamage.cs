using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDamage : MonoBehaviour
{
    public float damage = 20f;
    public LayerMask targetLayers;
    private HashSet<GameObject> alreadyHit = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Slash touched: " + other.name);

        if ((targetLayers.value & (1 << other.gameObject.layer)) == 0) return;

        if (!alreadyHit.Contains(other.gameObject))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Enemy found, applying damage");
                enemy.Damage(damage);
                alreadyHit.Add(other.gameObject);
            }
            else
            {
                Debug.Log("No Enemy component found on: " + other.name);
            }
        }
    }
}
