using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }
        public bool debug;

        public PlayerStateMachine(bool debug)
        {
            this.debug = debug;
        }

        public void Initialize(PlayerState initialState)
        {
            if (debug)
                Debug.Log("State machine started");

            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(PlayerState newState)
        {
            if (debug)
                Debug.Log($"ChangeState from: {CurrentState.GetType().Name}, to: {newState.GetType().Name}");

            CurrentState.Exit();
            CurrentState = newState;
            newState.Enter();
        }
    }
}
