using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerWallJumpState : PlayerAbilityState
    {

        private int wallJumpDirection;

        public PlayerWallJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.InputHandler.UseJumpInput();
            player.JumpState.ResetAmountOfJumps();
            player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
            player.FlipIfNeeded(wallJumpDirection);
            player.JumpState.DecreaseAmountOfJumps();
            player.PlayJumpSound();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= startTime + playerData.wallJumpTime)
            {
                isAbilityDone = true;
            }
        }

        public void DetermineWallJumpDirection(bool isTouchingWall)
        {
            if (isTouchingWall)
            {
                wallJumpDirection = -player.FacingDirection;
            }
            else
            {
                wallJumpDirection = player.FacingDirection;
            }
        }
    }
}