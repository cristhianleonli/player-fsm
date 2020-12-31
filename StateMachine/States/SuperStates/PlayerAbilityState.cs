using UnityEngine;

namespace Player.StateMachine.States
{
    public class PlayerAbilityState : PlayerState
    {
        protected bool isAbilityDone;
        private bool isGrounded;

        public PlayerAbilityState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

            isAbilityDone = false;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isAbilityDone)
            {
                if (isGrounded && player.CurrentVelocity.y <= 0.01f)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else
                {
                    stateMachine.ChangeState(player.InAirState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }

}
