using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwordState : SwordState
{
    public FireSwordState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    private Collider hitCollider;
    public override void ActivateSwordVisual()
    {
        player.SetActiveSwordModel(SwordPickup.SwordType.Fire);
        player.SpawnVFX("FireAura");
    }

    public override void EnableDamage()
    {
        hitCollider.enabled = true;
    }

    public override void DisableDamage()
    {
        hitCollider.enabled = false;
    }

    public override void ApplyElementEffect(Enemy enemy)
    {
        enemy.ApplyBurnEffect(); // dégâts dans le temps
    }

    public override void Enter()
    {
        base.Enter();
        player.fireDamageImmune = true; // Pour le blocage
    }

    public override void Exit()
    {
        player.fireDamageImmune = false;
    }
}

