using Player.StateMachine;
using Player.StateMachine.States;
using UnityEngine;

namespace Player
{

    enum FacingDirection
    {
        RIGHT = 1,
        LEFT = -1
    }

    public class PlayerController : MonoBehaviour
    {
        #region State machine
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerLandState LandState { get; private set; }
        #endregion

        public Animator Animator { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public Vector2 CurrentVelocity { get; private set; }

        private FacingDirection facingDirection = FacingDirection.RIGHT;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private PlayerData playerData;

        #region Life cycle
        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            CreateStates();
        }

        void Start()
        {
            Animator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            Rigidbody = GetComponent<Rigidbody2D>();

            // start the state machine with the idle state
            StateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            // keep the reference to the latest velocity the rigidbody has
            CurrentVelocity = Rigidbody.velocity;

            // let the state machine do its job with the current state
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            // let the state machine do its job with the current state
            StateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion

        private void CreateStates()
        {
            IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
            RunState = new PlayerRunState(this, StateMachine, playerData, "run");
            JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
            LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        }

        public void FlipIdNeeded(int inputX)
        {
            // only flip the player when the facing direction has changed
            if (inputX != 0 && inputX != (int)facingDirection)
            {
                Flip();
            }
        }

        public void SetVelocityX(float velocity)
        {
            // update the velocity in X of the player
            Rigidbody.velocity = new Vector2(velocity, Rigidbody.velocity.y);
        }

        public void SetVelocityY(float velocity)
        {
            // update the velocity in Y of the player
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, velocity);
        }

        public bool CheckIfGrounded()
        {
            // if the ground check overlaps with the ground wihtin a radius of a circle
            return Physics2D.OverlapCircle(groundCheck.position, playerData.GroundCheckRadius, playerData.GroundLayer);
        }

        private void Flip()
        {
            // toggle the facing direction
            facingDirection = (facingDirection == FacingDirection.RIGHT) ? FacingDirection.LEFT : FacingDirection.RIGHT;

            // apply roration to the game object
            transform.Rotate(0f, 180f, 0f);
        }

        public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    }
}