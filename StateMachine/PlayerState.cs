using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.StateMachine
{
    public class PlayerState
    {
        #region Inherited properties
        protected PlayerController player;
        protected PlayerStateMachine stateMachine;
        protected PlayerData playerData;
        protected float startTime;
        protected int InputX;
        protected bool InputJump;
        protected bool isExitingState;
        #endregion

        private readonly string animationName;

        public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.playerData = playerData;
            this.animationName = animationName;
        }

        public virtual void Enter()
        {
            RunChecks();

            player.Animator.Play(animationName);
            startTime = Time.time;
            isExitingState = false;
        }

        public virtual void Exit()
        {
            isExitingState = true;
        }

        public virtual void LogicUpdate()
        {
            InputX = player.InputHandler.InputX;
            InputJump = player.InputHandler.JumpInput;
        }

        public virtual void PhysicsUpdate()
        {
            RunChecks();
        }

        public virtual void RunChecks() { }

        public virtual void AnimationFinishTrigger() { }
    }
}