using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }

    private void Start()
    {
        MovementInput = new Vector2(0, 0);
    }

    private void Update()
    {
        MovementInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        MovementInput = MovementInput.normalized;
        NormalizedInputX = (int)MovementInput.x;
        NormalizedInputY = (int)MovementInput.y;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput = true;
        }
    }

    public void useJumpInput() => JumpInput = false;
}
