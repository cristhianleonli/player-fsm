using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }

        public void Initialize(PlayerState initialState)
        {
            Debug.Log("State machine started");
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(PlayerState newState)
        {
            Debug.Log($"ChangeState from: {CurrentState.GetType().Name}, to: {newState.GetType().Name}");
            CurrentState.Exit();
            CurrentState = newState;
            newState.Enter();
        }
    }
}
