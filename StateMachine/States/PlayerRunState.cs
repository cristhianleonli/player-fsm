using Player;
using Player.StateMachine;

public class PlayerRunState  : PlayerState
{
    public PlayerRunState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.FlipIdNeeded(InputX);
        player.SetVelocityX(playerData.RunSpeed * InputX);

        if (InputX == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (InputJump)
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
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