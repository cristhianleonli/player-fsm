using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    /// All this behavior can be replaced with any other
    /// user input mechanism such as the New Input System, but the
    /// logic onwards won't be affected(too much).

    public int InputX { get; private set; }
    public int InputY { get; private set; }
    public bool JumpInput { get; private set; }

    private void Update()
    {
        // get the normalized input
        Vector2 movementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        InputX = (int)movementInput.x;
        InputY = (int)movementInput.y;

        // jump key events
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput = true;
        }
    }

    public void UseJumpInput()
    {
        JumpInput = false;
    }
}