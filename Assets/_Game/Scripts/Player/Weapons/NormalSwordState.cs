using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSwordState : SwordState
{
    public NormalSwordState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    private Collider hitCollider;

    private float damageAmount = 10f;
    public override void ActivateSwordVisual()
    {
        player.SetActiveSwordModel(SwordPickup.SwordType.Normal);
    }

    public void SetHitCollider(Collider collider)
    {
        hitCollider = collider;
        if (hitCollider != null)
            hitCollider.enabled = false;  // Par défaut désactivé
    }

    public override void ApplyElementEffect(Enemy enemy)
    {
        
    }

    public override void OnHitEnemy (Enemy enemy)
    {
        enemy.Damage(damageAmount);
    }

    public override void EnableDamage()
    {
        if (hitCollider != null)
            hitCollider.enabled = true;
    }

    public override void DisableDamage()
    {
        if (hitCollider != null)
            hitCollider.enabled = false;
    }
}

