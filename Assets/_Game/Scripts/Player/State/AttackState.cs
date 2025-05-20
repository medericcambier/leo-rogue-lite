using UnityEngine;

public class AttackState : PlayerState
{
    private int comboIndex = 0;
    private float comboTimer;
    private bool canCombo;

    public AttackState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        comboIndex = 0;
        PlayAttackAnimation(comboIndex);
        canCombo = false;
        comboTimer = 0f;

        // Emp�che le mouvement complet ou partiel ici si n�cessaire
        player.BlockMovement();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetMouseButtonDown(0) && canCombo)
        {
            comboIndex++;
            PlayAttackAnimation(comboIndex);
            canCombo = false;
        }
    }

    public override void Update()
    {
        base.Update();
        comboTimer += Time.deltaTime;

        if (comboTimer >= 0.5f) // Dur�e minimale avant combo
        {
            canCombo = true;
        }

        // Ici tu v�rifies si l'animation d'attaque est termin�e
        if (player.IsAttackAnimationFinished())
        {
            stateMachine.ChangeState(new IdleState(player, stateMachine));
        }
    }

    private void PlayAttackAnimation(int index)
    {
        player.animator.SetTrigger("Attack" + index);
        player.EnableDamageForCurrentWeapon(); // M�thode personnalis�e
        comboTimer = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        player.BlockMovement();
    }
}

