using UnityEngine;
using Player.Audio;
using Player.ParticleEffects;
using Player.StateMachine;
using Player.StateMachine.States;
using System.Threading;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public Animator animator;

        #region State machine
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerLandState LandState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        #endregion

        public PlayerInputHandler InputHandler { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public Vector2 CurrentVelocity { get; private set; }
        public int FacingDirection { get; private set; }

        private Vector2 workspace;
        private float changeTime;

        [SerializeField] private AudioManager audioManager;
        [SerializeField] private ParticlesManager particlesManager;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;

        #region life cycle
        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            CreateStates();
        }

        void Start()
        {
            FacingDirection = 1;

            animator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            rb = GetComponent<Rigidbody2D>();

            StateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            CurrentVelocity = rb.velocity;
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion

        private void CreateStates()
        {
            IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
            MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
            JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
            InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
            LandState = new PlayerLandState(this, StateMachine, playerData, "land");
            WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
            WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
            DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        }

        public void SetVelocityX(float velocity)
        {
            workspace.Set(velocity, CurrentVelocity.y);
            rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityY(float velocity)
        {
            workspace.Set(CurrentVelocity.x, velocity);
            rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            rb.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public bool IsTouchingGround()
        {
            return Physics2D.OverlapCircle(
                groundCheck.position,
                playerData.groundCheckRadius,
                playerData.whatIsGround
            );
        }

        public bool IsTouchingWall()
        {
            return Physics2D.Raycast(
                wallCheck.position,
                Vector2.right * FacingDirection,
                playerData.wallCheckDistance,
                playerData.whatIsGround
            );
        }

        public bool IsTouchingWallBack()
        {
            return Physics2D.Raycast(
                wallCheck.position,
                Vector2.right * -FacingDirection,
                playerData.wallCheckDistance,
                playerData.whatIsGround
            );
        }

        public void FlipIfNeeded(int inputX)
        {
            if (inputX != 0 && inputX != FacingDirection)
            {
                FacingDirection *= -1;
                transform.Rotate(0, 180, 0);
            }
        }

        public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

        #region Particle effects
        public void ShowJumpDust() => particlesManager.ShowJumpDust();
        public void ShowTrail() => particlesManager.ShowTrail();
        public void HideTrail() => particlesManager.HideTrail();
        public void ShowLandDust() => particlesManager.ShowLandDust();
        public void ShowDashDust() => particlesManager.ShowDashDust();
        #endregion

        #region Sound effects
        public void PlayWalkSound() => audioManager.PlayWalkSound();
        public void StopWalkSound() => audioManager.StopWalkSound();
        public void PlayLandSound() => audioManager.PlayLandSound();
        public void PlayJumpSound() => audioManager.PlayJumpSound();
        public void PlayDashSound() => audioManager.PlayDashSound();
        #endregion
    }
}