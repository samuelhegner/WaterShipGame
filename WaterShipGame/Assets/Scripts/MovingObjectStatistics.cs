using UnityEngine;

public class MovingObjectStatistics : ScriptableObject
{
    [Header("Movement Statistics")]
    public float velocityDamping = 0.2f;
    public ForceMode forceModeToUse;
    public float minimumBounceSpeed = 10f;
}
