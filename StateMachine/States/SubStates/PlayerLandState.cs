using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerLandState : PlayerGroundedState
    {
        public PlayerLandState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!isExitingState)
            {
                if (inputX != 0)
                {
                    stateMachine.ChangeState(player.MoveState);
                }
                else if (isAnimationFinished)
                {
                    stateMachine.ChangeState(player.IdleState);
                }

                player.ShowLandDust();
                player.PlayLandSound();
            }
        }
    }
}