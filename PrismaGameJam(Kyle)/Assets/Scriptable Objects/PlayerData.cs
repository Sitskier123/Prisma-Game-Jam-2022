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
}
