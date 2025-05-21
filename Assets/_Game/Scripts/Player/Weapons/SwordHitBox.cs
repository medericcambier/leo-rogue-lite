using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public SwordState swordState;

    private PlayerController player;

    void Start()
    {
        // Récupère automatiquement le PlayerController dans un parent direct ou indirect
        player = GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && player.currentSwordState != null)
            {
                player.currentSwordState.OnHitEnemy(enemy);
            }
        }
    }
}