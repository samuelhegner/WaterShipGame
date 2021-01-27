using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Ship Stats", menuName = "Player Ship Statistics")]
public class PlayerShipStatistics : ScriptableObject
{
    [Header("Ship Turning Statistics")]
    public bool turnInMovementDirection = true;
    [Range(1, 20)] public float turnSpeed = 5;
}
