using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.animator.SetFloat("ForwardSpeed", 0);
        player.animator.SetFloat("StrafeSpeed", 0);
    }

    public override void Update()
    {
        player.RotateCamera();
        player.ApplyGravity();
    }

    public override void FixedUpdate()
    {
        player.rb.velocity = new Vector3(0f, player.rb.velocity.y, 0f); // Pas de déplacement
    }

    public override void HandleInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            stateMachine.ChangeState(new MoveState(player, stateMachine));
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            stateMachine.ChangeState(new BlockState(player, stateMachine));
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(new AttackState(player, stateMachine));
            return;
        }
    }
}

