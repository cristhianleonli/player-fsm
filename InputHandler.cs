using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public int InputX { get; private set; }
    public int InputY { get; private set; }

    public bool InputJump { get; private set; }
    public bool InputCrouch { get; private set; }

    private void Update()
    {
        Vector2 movementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        InputX = (int)movementInput.x;
        InputY = (int)movementInput.y;

        // jump key events
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputJump = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            InputCrouch = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            InputCrouch = false;
        }
    }

    public void UseJumpInput()
    {
        InputJump = false;
    }
}
