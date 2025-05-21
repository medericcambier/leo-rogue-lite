using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwordState : PlayerState
{
    protected Collider hitCollider;
    public SwordState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public abstract void ApplyElementEffect(Enemy enemy); // À implémenter dans les sous-classes

    public abstract void ActivateSwordVisual(); // VFX et modèles

    public override void Enter()
    {
        ActivateSwordVisual();
    }

    public virtual void EnableDamage()
    {
        // Sera surchargé par les états spécifiques d'épée
    }

    public virtual void DisableDamage()
    {
        // Sera surchargé par les états spécifiques d'épée
    }

    public virtual void OnHitEnemy(Enemy enemy)
    {
        // Par défaut rien, les sous-classes peuvent override
    }

    public virtual void SetHitCollider(Collider collider)
    {
        hitCollider = collider;
        if (hitCollider != null)
            hitCollider.enabled = false; // par défaut désactivé
    }



    public override void Update()
    {
        player.RotateCamera();
        player.ApplyGravity();

        // Changement d'état de déplacement ou blocage
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(1))
        {
            stateMachine.ChangeState(new BlockState(player, stateMachine));
            return;
        }

        if (h != 0 || v != 0)
        {
            stateMachine.ChangeState(new MoveState(player, stateMachine));
            return;
        }
        else
        {
            stateMachine.ChangeState(new IdleState(player, stateMachine));
            return;
        }
    }
}
