using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerDashState : PlayerAbilityState
    {

        private float lastDashTime;
        private Vector2 holdPosition;
        private bool canPerformDash;

        public PlayerDashState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ShowDashDust();
            player.PlayDashSound();
            player.SetVelocity(playerData.dashSpeed, Vector2.right, player.FacingDirection);
            holdPosition = player.transform.position;
            player.InputHandler.UseDashInput();
            canPerformDash = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= startTime + playerData.dashDuration)
            {
                isAbilityDone = true;
                lastDashTime = Time.time;
            }
            else
            {
                player.transform.position = new Vector2(player.transform.position.x, holdPosition.y);
                player.SetVelocityY(0);
            }
        }

        public bool CanDash()
        {
            return canPerformDash && Time.time >= lastDashTime + playerData.dashCooldown;
        }

        public void ResetDash() => canPerformDash = true;
    }

}