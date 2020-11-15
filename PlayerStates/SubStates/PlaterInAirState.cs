using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaterInAirState : PlayerState
{
    public PlaterInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }
}
