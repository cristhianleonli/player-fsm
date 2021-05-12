using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    // These values fit great for a Rigidbody with GravityScale 1
    public LayerMask GroundLayer;

    public float GroundCheckRadius = 0.3f;
    public float RunSpeed = 2f;
    public float JumpSpeed = 3f;
    public float RunSpeedWhileJumping = 1.5f;

    public float ObjectImpulseForce = 8f;
}