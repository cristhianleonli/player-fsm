using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Animator.Play("PlayerIdle");
        Player.InputHandler.UseJumpInput();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (InputX != 0)
        {
            StateMachine.ChangeState(Player.RunningState);
        }

        if (InputJump)
        {
            Player.InputHandler.UseJumpInput();
            StateMachine.ChangeState(Player.JumpingState);
        }

        if (InputCrouch)
        {
            StateMachine.ChangeState(Player.CrouchingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // public override void EnterState(PlayerController player)
    // {
    //     player.Animator.Play("PlayerIdle");
    // }
    //
    // public override void Update(PlayerController player)
    // {
    //     // Debug.Log("update idle state");
    //     
    //     // if (isGrounded == false)
    //     // { 
    //     //     player.JumpingState.Update(player);            
    //     // }
    //     
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         player.Rigidbody.velocity = Vector2.up * 15;
    //         player.TransitionToState(player.JumpingState);
    //     }
    //     
    //     // if (isGrounded && Input.GetKeyDown(KeyCode.Space))
    //     // {
    //     //     player.TransitionToState(player.JumpingState);
    //     // }
    //     
    //     // if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
    //     // {
    //     //     player.TransitionToState(player.RunningState);
    //     // }
    //
    //     if (player.moveInput != 0)
    //     {
    //         player.TransitionToState(player.RunningState);
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.DownArrow))
    //     {
    //         player.TransitionToState(player.CrouchingState);
    //     }
    // }
}