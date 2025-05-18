using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSwordState : SwordState
{
    public NormalSwordState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void ActivateSwordVisual()
    {
        player.SetActiveSwordModel(SwordPickup.SwordType.Normal);
    }

    public override void ApplyElementEffect(Enemy enemy)
    {
        enemy.TakeBaseDamage();
    }
}

