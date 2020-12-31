using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerState
    {
        protected PlayerController player;
        protected PlayerStateMachine stateMachine;
        protected PlayerData playerData;
        protected float startTime;
        protected bool isAnimationFinished;
        protected bool isExitingState;

        private string animBoolName;

        protected bool dashInput;

        public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.playerData = playerData;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            RunChecks();

            player.animator.SetBool(animBoolName, true);
            startTime = Time.time;
            isAnimationFinished = false;
            isExitingState = false;
        }

        public virtual void Exit()
        {
            player.animator.SetBool(animBoolName, false);
            isExitingState = true;
        }

        public virtual void LogicUpdate()
        {
            dashInput = player.InputHandler.DashInput;
        }

        public virtual void PhysicsUpdate()
        {
            RunChecks();
        }

        public virtual void RunChecks() { }

        public virtual void AnimationStartTrigger() { }

        public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    }

}