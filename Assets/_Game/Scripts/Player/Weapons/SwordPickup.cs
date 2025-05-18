using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public enum SwordType { Normal, Fire, Ice, Lightning }
    public SwordType swordType;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerController player = FindObjectOfType<PlayerController>();

            switch (swordType)
            {
                case SwordType.Normal:
                    player.SetSwordState(new NormalSwordState(player, player.stateMachine));
                    break;
                case SwordType.Fire:
                    player.SetSwordState(new FireSwordState(player, player.stateMachine));
                    break;
                case SwordType.Ice:
                    player.SetSwordState(new IceSwordState(player, player.stateMachine));
                    break;
                case SwordType.Lightning:
                    player.SetSwordState(new LightningSwordState(player, player.stateMachine));
                    break;
            }

            Debug.Log("Changé en : " + swordType);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

