using UnityEngine;

public class AttackState : PlayerState
{
    private int comboIndex = 0;
    private float comboTimer;
    private bool canCombo;
    private bool hasActivatedDamage;

    public AttackState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        comboIndex = 0;
        PlayAttackAnimation(comboIndex);
        canCombo = false;
        comboTimer = 0f;
        hasActivatedDamage = false;

        player.BlockMovement(); // Empêche le mouvement pendant l’attaque
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (Input.GetMouseButtonDown(0) && canCombo)
        {
            comboIndex++;
            PlayAttackAnimation(comboIndex);
            canCombo = false;
            hasActivatedDamage = false; // Pour relancer le trigger de dégâts sur le prochain coup
        }
    }

    public override void Update()
    {
        base.Update();

        player.RotateCamera();
        player.ApplyGravity();

        comboTimer += Time.deltaTime;

        if (comboTimer >= 0.5f)
            canCombo = true;

        // Gestion de l'activation des dégâts
        float animTime = player.GetCurrentAnimationTime("Attack" + comboIndex);
        if (!hasActivatedDamage && animTime >= 0.3f) // Ajuste ce timing selon ton animation
        {
            player.currentSwordState.EnableDamage(); // Chaque état d’épée gère son propre effet
            hasActivatedDamage = true;
        }

        // Retour à l’état Idle si l’animation est terminée
        if (player.IsAttackAnimationFinished("Attack" + comboIndex))
        {
            player.UnblockMovement();
            player.DisableSwordCollider(); // Facultatif, selon ton système
            stateMachine.ChangeState(new IdleState(player, stateMachine));
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.DisableSwordCollider(); // Sécurité pour désactiver les dégâts
        player.UnblockMovement();
    }

    private void PlayAttackAnimation(int index)
    {
        player.animator.SetTrigger("Attack" + index);
        comboTimer = 0f;
    }
}


