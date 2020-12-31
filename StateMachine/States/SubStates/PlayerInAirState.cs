using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerInAirState : PlayerState
    {
        // Input
        private int inputX;
        private bool jumpInput;
        private bool jumpInputStop;

        // Checks
        private bool isGrounded;
        private bool isTouchingWall;
        private bool isTouchingWallBack;
        private bool oldIsTouchingWall;
        private bool oldIsTouchingWallBack;

        private bool coyoteTime;
        private bool wallJumpCoyoteTime;
        private bool isJumping;

        private float startWallJumpCoyoteTime;

        public PlayerInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void RunChecks()
        {
            base.RunChecks();

            oldIsTouchingWall = isTouchingWall;
            oldIsTouchingWallBack = isTouchingWallBack;

            isGrounded = player.IsTouchingGround();
            isTouchingWall = player.IsTouchingWall();
            isTouchingWallBack = player.IsTouchingWallBack();

            if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
            {
                StartWallJumpCoyoteTime();
            }
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            oldIsTouchingWall = false;
            oldIsTouchingWallBack = false;
            isTouchingWall = false;
            isTouchingWallBack = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            CheckCoyoteTime();
            CheckWallJumpCoyoteTime();

            inputX = player.InputHandler.NormalizedInputX;
            jumpInput = player.InputHandler.JumpInput;
            jumpInputStop = player.InputHandler.JumpInputStop;

            CheckJumpMultiplier();

            if (isGrounded && player.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.LandState);
            }
            else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
            {
                StopWallJumpCoyoteTime();
                isTouchingWall = player.IsTouchingWall();
                player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
                stateMachine.ChangeState(player.WallJumpState);
            }
            else if (jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (isTouchingWall && inputX == player.FacingDirection && player.CurrentVelocity.y <= 0)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
            else if (dashInput && player.DashState.CanDash())
            {
                stateMachine.ChangeState(player.DashState);
            }
            else
            {
                player.FlipIfNeeded(inputX);
                player.SetVelocityX(playerData.movementVelocity * inputX);
            }
        }

        private void CheckJumpMultiplier()
        {
            if (isJumping)
            {
                if (jumpInputStop)
                {
                    player.SetVelocityY(player.CurrentVelocity.y * playerData.jumpHeightMultiplier);
                    isJumping = false;
                }
                else if (player.CurrentVelocity.y <= 0f)
                {
                    isJumping = false;
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private void CheckCoyoteTime()
        {
            if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
            {
                coyoteTime = false;
                player.JumpState.DecreaseAmountOfJumps();
            }
        }

        private void CheckWallJumpCoyoteTime()
        {
            if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
            {
                wallJumpCoyoteTime = false;
            }
        }

        public void StartCoyoteTime() => coyoteTime = true;

        public void StartWallJumpCoyoteTime()
        {
            wallJumpCoyoteTime = true;
            startWallJumpCoyoteTime = Time.time;
        }

        public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

        public void SetIsJumping() => isJumping = true;
    }
}