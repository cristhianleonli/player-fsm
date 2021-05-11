using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
        {
        }

        public override void RunChecks()
        {
            base.RunChecks();
        }

        public override void Enter()
        {
            base.Enter();
            player.SetVelocityX(0f);

            // avoid any saved jump input
            player.InputHandler.UseJumpInput();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // when the player starts moving, change the state to Run
            if (InputX != 0)
            {
                stateMachine.ChangeState(player.RunState);
            }

            // Once the player hit the jump input, change the state to Jump
            if (InputJump)
            {
                player.InputHandler.UseJumpInput();
                stateMachine.ChangeState(player.JumpState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}