using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerJumpState : PlayerAbilityState
    {
        private int amountOfJumpsLeft;

        public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
            amountOfJumpsLeft = playerData.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();

            player.InputHandler.UseJumpInput();
            player.SetVelocityY(playerData.jumpVelocity);
            isAbilityDone = true;

            DecreaseAmountOfJumps();
            player.InAirState.SetIsJumping();
            player.ShowJumpDust();
            player.PlayJumpSound();
        }

        public bool CanJump()
        {
            if (amountOfJumpsLeft > 0)
            {
                return true;
            }

            return false;
        }

        public void ResetAmountOfJumps() => amountOfJumpsLeft = playerData.amountOfJumps;
        public void DecreaseAmountOfJumps() => amountOfJumpsLeft--;
    }
}