using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Ship Stats", menuName = "Enemy Ship Statistics")]
public class EnemyShipStatistics : ScriptableObject
{
    [Header("Movement Statistics")]
    public float speedWhileTurning = 5f;
    public float speedWhileCharging = 10f;
    public float brakeSpeed = 2f;
    public float accelerationSpeed = 5f;
    public float distanceToOvershootCharge = 5f;

    [Header("Rotation Statistics")]
    [Range(0, 180)] public float turnSpeed;

    [Header("Awareness Statistics")]
    public float radiusOfAwareness = 15;


}
