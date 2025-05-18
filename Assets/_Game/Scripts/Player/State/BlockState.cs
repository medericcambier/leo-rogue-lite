using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : PlayerState
{
    public bool fireDamageImmune;
    public bool iceDamageImmune;
    public BlockState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("EnteringBlockState");
        player.animator.SetBool("IsBlocking", true);
    }

    public override void Exit()
    {
        Debug.Log("OutBlockState");
        player.animator.SetBool("IsBlocking", false);
    }

    public override void Update()
    {
        player.RotateCamera();
        player.ApplyGravity();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float strafeFactor = 0.9f;
        Vector3 move = player.transform.right * h * strafeFactor + player.transform.forward * v;
        player.currentVelocity = Vector3.Lerp(player.currentVelocity, move * (player.moveSpeed * 0.5f), player.acceleration * Time.deltaTime);

        Vector3 localVel = player.transform.InverseTransformDirection(player.rb.velocity);
        player.animator.SetFloat("ForwardSpeed", localVel.z);
        player.animator.SetFloat("StrafeSpeed", localVel.x);

        // Sortie du blocage
        if (Input.GetMouseButtonUp(1))
        {
            stateMachine.ChangeState(h != 0 || v != 0 ? new MoveState(player, stateMachine) : new IdleState(player, stateMachine));
            return;
        }
    }

    public override void FixedUpdate()
    {
        player.rb.velocity = new Vector3(player.currentVelocity.x, player.rb.velocity.y, player.currentVelocity.z);
    }
}

