using System.Collections;
using Unity;
using UnityEngine;

public class ShipMover : PushableObject
{
    public void stunForSeconds(float seconds)
    {
        StartCoroutine(sleepTimer(seconds));
    }
    IEnumerator sleepTimer(float secondsToSleepFor)
    {
        objectRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(secondsToSleepFor);
        objectRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

}
