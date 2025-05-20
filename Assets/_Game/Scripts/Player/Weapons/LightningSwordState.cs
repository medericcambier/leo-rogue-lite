using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSwordState : SwordState
{
    public LightningSwordState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void ActivateSwordVisual()
    {
        player.SetActiveSwordModel(SwordPickup.SwordType.Lightning);
        player.SpawnVFX("LightningAura");
    }

    public override void ApplyElementEffect(Enemy enemy)
    {
        enemy.ChainLightningEffect(); // Attaque en cha�ne
    }

    public override void EnableDamage()
    {
        // ta logique ici
    }
}

