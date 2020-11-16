using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData: ScriptableObject
{
    public LayerMask whatIsGround;
    public float checkGroundRadius = 0.3f;
    public float runSpeed = 7.5f;
    public float jumpSpeed = 16.5f;
    public float runSpeedWhileJumping = 6;
}
