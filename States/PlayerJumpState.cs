using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Animator.Play("PlayerJump");
        Player.SetYVelocity(PlayerData.jumpSpeed);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Player.CheckIfGrounded() && Player.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.IdleState);
        }

        if (InputX == 0)
        {
            Player.SetXVelocity(0);
        } else {
            Player.CheckIfShouldFlip(InputX);
            Player.SetXVelocity(PlayerData.runSpeedWhileJumping * InputX);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
