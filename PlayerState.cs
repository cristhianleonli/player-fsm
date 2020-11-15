using UnityEngine;

public class PlayerState

{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;
    private string animationName;

    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationName = animationName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Animator.SetBool(animationName, true);
        startTime = Time.time;
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animationName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
