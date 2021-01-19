using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ripple Stats", menuName = "Ripple Statistics")]
public class RippleStatistics : ScriptableObject
{
    [Header("Spread Statistics")]
    [Range(0.1f, 20f)]public float sizeIncreaseSpeed;
    [Range(0.1f, 20f)] public float maximumRippleSize;

    [Header("Force Statistics")]
    public float maximumPushForce;
    public float minimumPushForce;
}
