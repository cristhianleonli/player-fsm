using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerTouchingWallState : PlayerState
    {

        protected bool isGrounded;
        protected bool isTouchingWall;
        protected int inputX;
        protected int inputY;
        protected bool jumpInput;

        public PlayerTouchingWallState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }

        public override void AnimationStartTrigger()
        {
            base.AnimationStartTrigger();
        }

        public override void RunChecks()
        {
            base.RunChecks();

            isGrounded = player.IsTouchingGround();
            isTouchingWall = player.IsTouchingWall();
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

            inputX = player.InputHandler.NormalizedInputX;
            inputY = player.InputHandler.NormalizedInputY;
            jumpInput = player.InputHandler.JumpInput;

            if (jumpInput)
            {
                player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
                stateMachine.ChangeState(player.WallJumpState);
            }
            else if (isGrounded)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (!isTouchingWall || inputX != player.FacingDirection)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }

}