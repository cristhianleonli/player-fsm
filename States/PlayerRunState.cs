using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Animator.Play("PlayerRun");
        
        if (isDebugEnabled)
        {
            Debug.Log("enter run");            
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.CheckIfShouldFlip(InputX);
        Player.SetXVelocity(PlayerData.runSpeed * InputX);
        
        if (InputX == 0)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        
        if (InputJump)
        {
            Player.InputHandler.UseJumpInput();
            StateMachine.ChangeState(Player.JumpingState);
        }
    }

    // public override void EnterState(PlayerController player)
    // {
    //     player.Animator.Play("PlayerRun");
    // }
    //
    // public override void Update(PlayerController player)
    // {
    //     if (player.moveInput == 0)
    //     {
    //         player.Rigidbody.velocity = new Vector2(0, player.Rigidbody.velocity.y);
    //         player.TransitionToState(player.IdleState);
    //     }
    //     else
    //     {
    //         player.Rigidbody.velocity = new Vector2(player.moveInput * 8, player.Rigidbody.velocity.y);
    //         player.Renderer.flipX = player.moveInput < 0;
    //     }
    //
    //     if (player.isGrounded && Input.GetKeyUp(KeyCode.Space))
    //     {
    //         player.Rigidbody.velocity = Vector2.up * 15;
    //         player.TransitionToState(player.JumpingState);
    //     }
    // }
}
