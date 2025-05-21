using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwordState : PlayerState
{
    protected Collider hitCollider;
    public SwordState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public abstract void ApplyElementEffect(Enemy enemy); // � impl�menter dans les sous-classes

    public abstract void ActivateSwordVisual(); // VFX et mod�les

    public override void Enter()
    {
        ActivateSwordVisual();
    }

    public virtual void EnableDamage()
    {
        // Sera surcharg� par les �tats sp�cifiques d'�p�e
    }

    public virtual void DisableDamage()
    {
        // Sera surcharg� par les �tats sp�cifiques d'�p�e
    }

    public virtual void OnHitEnemy(Enemy enemy)
    {
        // Par d�faut rien, les sous-classes peuvent override
    }

    public virtual void SetHitCollider(Collider collider)
    {
        hitCollider = collider;
        if (hitCollider != null)
            hitCollider.enabled = false; // par d�faut d�sactiv�
    }



    public override void Update()
    {
        player.RotateCamera();
        player.ApplyGravity();

        // Changement d'�tat de d�placement ou blocage
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
