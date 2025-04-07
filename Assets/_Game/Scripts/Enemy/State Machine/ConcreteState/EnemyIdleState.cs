using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 _targetPos;
    private Vector3 _direction;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        _targetPos = GetRandomPointInBounds();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if(enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }

        _direction = (_targetPos - enemy.transform.position).normalized;

        enemy.MoveEnemy(_direction * enemy.RandomMovementSpeed);

        if ((enemy.transform.position -  _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInBounds();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    private Vector3 GetRandomPointInBounds()
    {
        if (enemy.MovementBounds == null)
        {
            Debug.LogWarning("MovementBounds not assigned on enemy!");
            return enemy.transform.position;
        }

        Vector3 min = enemy.MovementBounds.Min;
        Vector3 max = enemy.MovementBounds.Max;

        return new Vector3(
            Random.Range(min.x, max.x),
            enemy.transform.position.y, // On garde la même hauteur
            Random.Range(min.z, max.z)
        );
    }
}
