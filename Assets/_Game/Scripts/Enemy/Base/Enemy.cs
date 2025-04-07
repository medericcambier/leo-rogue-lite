using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;

    [field: SerializeField] public float RotationSpeed { get; set; } = 5f; // Vitesse de rotation de l'ennemi

    [field: SerializeField] public ColliderBounds MovementBounds { get; private set; }
    public float CurrentHealth { get; set; }
    public Rigidbody Rigidbody { get; set; }

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }

    public EnemyIdleState IdleState { get; set; }

    public EnemyChaseState ChaseState { get; set; }

    public EnemyAttackState AttackState { get; set; }
    public bool IsAggroed { get; set ; }
    public bool IsWithinStrikingDistance { get; set; }

    #endregion


    #region Idle Variables

    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;

    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState (this, StateMachine);   
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        Rigidbody = GetComponent<Rigidbody>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }


    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    #region HealthDie

    public void Damage(float damageAmount)
    {
        CurrentHealth = damageAmount;

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector3 direction)
    {
        // Calcule la direction vers laquelle l'ennemi doit se tourner (en ignorant la composante verticale)
        direction.y = 0; // Évite la rotation autour de l'axe vertical

        // Crée la rotation cible vers la direction du mouvement
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Applique une rotation fluide vers la direction cible
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);

        Rigidbody.MovePosition(Rigidbody.position + direction * Time.deltaTime);
    }

    #endregion

    #region Distance Check

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }

    #endregion

    #region Animation Trigger

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
    }

    #endregion
}
