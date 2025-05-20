using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwordState : PlayerState
{
    public SwordState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public abstract void ApplyElementEffect(Enemy enemy); // À implémenter dans les sous-classes

    public abstract void ActivateSwordVisual(); // VFX et modèles

    public override void Enter()
    {
        ActivateSwordVisual();
    }

    public virtual void EnableDamage() { }

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
