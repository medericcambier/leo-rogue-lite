using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform _playerTransform;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // V�rifie si le joueur est sorti de la port�e de frappe
        if (!enemy.IsWithinStrikingDistance)
        {
            // Si le joueur est sorti de la port�e, on revient en ChaseState
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return; // On quitte la m�thode pour �viter d'ex�cuter le reste
        }

        // Ici, tu peux ajouter ta logique d'attaque, si tu en as une

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
