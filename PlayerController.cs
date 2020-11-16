using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    
    public InputHandler InputHandler { get; private set; }
    #endregion

    #region State Machine
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerJumpState JumpingState { get; private set; }
    public PlayerRunState RunningState { get; private set; }
    public PlayerCrouchState CrouchingState { get; private set; }
    #endregion
    
    #region Other variables
    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private PlayerData playerData;
    private int facingDirection = -1;
    #endregion

    private void Awake()
    { 
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData);
        RunningState = new PlayerRunState(this, StateMachine, playerData);
        JumpingState = new PlayerJumpState(this, StateMachine, playerData);
        CrouchingState = new PlayerCrouchState(this, StateMachine, playerData);
    }

    private void Start()
    { 
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        InputHandler = GetComponent<InputHandler>();
        StateMachine.Initiliaze(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetXVelocity(float velocity)
    {
        Rigidbody.velocity = new Vector2(velocity, Rigidbody.velocity.y);
        workspace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }
    
    public void SetYVelocity(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void CheckIfShouldFlip(int inputX)
    {
        if (inputX != 0 && inputX != facingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.checkGroundRadius, playerData.whatIsGround);
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
}
