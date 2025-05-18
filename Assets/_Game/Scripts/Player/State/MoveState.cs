using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerState
{
    public MoveState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Update()
    {
        player.RotateCamera();
        player.ApplyGravity();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float strafeFactor = 0.9f;
        Vector3 move = player.transform.right * h * strafeFactor + player.transform.forward * v;
        player.currentVelocity = Vector3.Lerp(player.currentVelocity, move * player.moveSpeed, player.acceleration * Time.deltaTime);

        // Animation
        Vector3 localVel = player.transform.InverseTransformDirection(player.rb.velocity);
        player.animator.SetFloat("ForwardSpeed", localVel.z);
        player.animator.SetFloat("StrafeSpeed", localVel.x);

        if (Input.GetMouseButtonDown(1))
        {
            stateMachine.ChangeState(new BlockState(player, stateMachine));
            return;
        }

        if (h == 0 && v == 0)
        {
            stateMachine.ChangeState(new IdleState(player, stateMachine));
            return;
        }
    }

    public override void FixedUpdate()
    {
        player.rb.velocity = new Vector3(player.currentVelocity.x, player.rb.velocity.y, player.currentVelocity.z);
    }
}

