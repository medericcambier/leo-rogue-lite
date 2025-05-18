using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSwordState : SwordState
{
    public IceSwordState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void ActivateSwordVisual()
    {
        player.SetActiveSwordModel(SwordPickup.SwordType.Ice);
        player.SpawnVFX("IceAura");
    }

    public override void ApplyElementEffect(Enemy enemy)
    {
        enemy.ApplyFreezeEffect();
    }

    public override void Enter()
    {
        base.Enter();
        player.iceDamageImmune = true;
    }

    public override void Exit()
    {
        player.iceDamageImmune = false;
    }
}

