using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    //Physics
    [Header("Gravity")]
    public float gravityScale;
    public float fallGravityMult;
    public float quickFallGravityMult;

    [Header("Drag")]
    public float amountDrag;
    public float amountFriction;

    [Header("Other Physics")]
    [Range(0, 0.5f)] public float coyoteTime;

    //Ground
    [Header("Run")]
    public float runMaxSpeed;
    public float runAccel;
    public float runDeccel;
    [Range(0,1)] public float accelInAir;
    [Range(0,1)] public float deccelInAir;
    [Space(5)]
    [Range(0.5f, 2f)] public float accelPower;
    [Range(0.5f, 2f)] public float stopPower;
    [Range(0.5f, 2f)] public float turnPower;

    //Jump
    [Header("Jump")]
    public float jumpForce;
    [Range(0, 1)] public float jumpCutMultiplier;
    [Space(10)]
    [Range(0, 0.5f)] public float jumpBufferTime;

    //Other
    [Header("Other Settings")]
    public bool doKeepRunMomentum;
    public bool doTurnOnWallJump;

}
