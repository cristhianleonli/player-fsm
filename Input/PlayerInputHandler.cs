using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get;  private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }

    private bool canDash = true;
    private float movementTreshold = 0.5f;
    private float jumpInputStartTime;

    [SerializeField] private float inputHoldTime = 0.2f;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(RawMovementInput.x) > movementTreshold)
        {
            NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else {
            NormalizedInputX = 0;
        }

        if (Mathf.Abs(RawMovementInput.y) > movementTreshold)
        {
            NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormalizedInputY = 0;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
            JumpInputStop = false;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (canDash && context.started)
        {
            DashInput = true;
            canDash = false;
        }

        if (context.canceled)
        {
            DashInput = false;
            canDash = true;
        }
    }

    private void Update()
    {
        CheckJumpImputHoldTime();
    }

    private void CheckJumpImputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
}
