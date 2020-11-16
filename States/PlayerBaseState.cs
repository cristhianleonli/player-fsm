using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.Timeline;

public class PlayerBaseState
{
    protected PlayerController Player;
    protected PlayerStateMachine StateMachine;
    protected PlayerData PlayerData;
    
    protected int InputX;
    protected bool InputJump;
    protected bool InputCrouch;

    protected bool isDebugEnabled = false;

    public PlayerBaseState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData)
    {
        Player = player;
        StateMachine = stateMachine;
        PlayerData = playerData;
    }

    public virtual void DoChecks()
    {

    }

    public virtual void Enter()
    {
        DoChecks();
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        InputX = Player.InputHandler.InputX;
        InputJump = Player.InputHandler.InputJump;
        InputCrouch = Player.InputHandler.InputCrouch;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
} 
