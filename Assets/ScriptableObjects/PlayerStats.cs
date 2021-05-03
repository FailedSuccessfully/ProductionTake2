using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    public float maxSpeed, acceleration, deceleration, jumpForce, dashForce, boostForce, dashCooldown;
    [SerializeField]
    public int jumpNum;
}
