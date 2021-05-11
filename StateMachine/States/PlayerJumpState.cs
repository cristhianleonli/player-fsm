using Player;
using Player.StateMachine;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // right after the player changes to jump
        // the velocity y has to update to make it jump
        player.SetVelocityY(playerData.JumpSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // when the player is back to the ground with negative velocity, go to idle
        if (player.CheckIfGrounded() && player.CurrentVelocity.y < 0.01f)
        {
            // If the player doesn't have landing state, then set it to Idle directly.
            stateMachine.ChangeState(player.LandState);
        }

        // when the player isn't moving horizontally, set the X velocity to 0
        if (InputX == 0)
        {
            player.SetVelocityX(0);
        }
        else
        {
            // flip and move when the player is in the air
            player.FlipIdNeeded(InputX);
            player.SetVelocityX(playerData.RunSpeedWhileJumping * InputX);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void RunChecks()
    {
        base.RunChecks();
    }
}
