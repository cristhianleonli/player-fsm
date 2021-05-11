using Player;
using Player.StateMachine;
using UnityEngine;

public class PlayerLandState : PlayerState
{
    public PlayerLandState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void RunChecks()
    {
        base.RunChecks();
    }

    public override void AnimationFinishTrigger()
    {
        // onlly when the landing animation finished, we can move on to the Idle state
        stateMachine.ChangeState(player.IdleState);
    }
}
