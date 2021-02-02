using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pushing Stats", menuName = "Pushing Statistics")]
public class PushingStatistics : ScriptableObject
{
    public float maximumPushForce;
    public float minimumPushForce;
}