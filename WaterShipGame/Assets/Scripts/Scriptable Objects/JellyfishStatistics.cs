using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Jellyfish Stats", menuName = "Jellyfish Statistics")]
public class JellyfishStatistics : PushableObjectStatistics
{
    [Header("Waypoint Stats")]
    public float distanceBeforeWaypointReached = 1f;
    public float movementSpeed;

    [Header("Stun Stats")]
    public float stunDuration;

}