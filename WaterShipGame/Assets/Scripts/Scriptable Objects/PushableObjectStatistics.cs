using UnityEngine;

public class PushableObjectStatistics : ScriptableObject
{
    [Header("Movement Statistics")]
    public float velocityDamping = 0.2f;
    public ForceMode forceModeToUse;
    public float minimumBounceSpeed = 10f;
    public float maximumVelocity = 10f;
}
