using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ShowTrail();
            player.PlayWalkSound();
        }

        public override void Exit()
        {
            base.Exit();
            player.HideTrail();
            player.StopWalkSound();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            player.FlipIfNeeded(inputX);
            player.SetVelocityX(playerData.movementVelocity * inputX);

            if (inputX == 0 && !isExitingState)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}