using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerGroundedState : PlayerState
    {
        protected int inputX;
        private bool jumpInput;
        private bool isGrounded;

        public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void RunChecks()
        {
            base.RunChecks();

            isGrounded = player.IsTouchingGround();
        }

        public override void Enter()
        {
            base.Enter();

            player.JumpState.ResetAmountOfJumps();
            player.DashState.ResetDash();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            inputX = player.InputHandler.NormalizedInputX;
            jumpInput = player.InputHandler.JumpInput;

            if (jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (!isGrounded)
            {
                player.InAirState.StartCoyoteTime();
                stateMachine.ChangeState(player.InAirState);
            }
            else if (dashInput && player.DashState.CanDash())
            {
                stateMachine.ChangeState(player.DashState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
