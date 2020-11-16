using UnityEngine;

public class PlayerStateMachine
{
    public PlayerBaseState CurrentState { get; private set; }

    public void Initiliaze(PlayerBaseState initialState)
    {
        CurrentState = initialState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerBaseState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
