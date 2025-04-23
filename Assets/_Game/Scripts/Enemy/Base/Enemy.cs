using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;

    public float CurrentHealth { get; set; } = 5f;
    public Image healthBar;

    [field: SerializeField] public float RotationSpeed { get; protected set; }
    [field: SerializeField] public ColliderBounds MovementBounds { get; private set; }
    public Rigidbody Rigidbody { get; set; }
    public Animator Animator { get; private set; }


    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public bool IsAggroed { get; set ; }
    public bool IsWithinStrikingDistance { get; set; }

    #endregion


    #region Idle Variables

    public float RandomMovementSpeed = 5f;

    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState (this, StateMachine);

        CurrentHealth = MaxHealth;

        Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {

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
        CurrentHealth -= damageAmount;

        healthBar.fillAmount = CurrentHealth / MaxHealth;

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Animator.SetBool("IsWalking", false);
        Animator.SetBool("IsDead", true);
        Destroy(gameObject, 1f);
    }

    #endregion

    #region Movement Functions

    public virtual void MoveEnemy(Vector3 direction)
    {
        // Calcule la direction vers laquelle l'ennemi doit se tourner (en ignorant la composante verticale)
        direction.y = 0; // Évite la rotation autour de l'axe vertical

        // Crée la rotation cible vers la direction du mouvement
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Applique une rotation fluide vers la direction cible
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);

        Rigidbody.MovePosition(Rigidbody.position + direction * Time.deltaTime);

        Animator.SetBool("IsWalking", true);
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
