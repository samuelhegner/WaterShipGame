using System.Collections;
using UnityEngine;


public class WavePusher : ObjectPusher
{
    WaveMover mover;

    private void Start()
    {
        mover = GetComponent<WaveMover>();
    }

    private void OnTriggerStay(Collider other)
    {
        
        PushableObject pushableObject = other.GetComponent<PushableObject>();

        if (colliderIsNotPushable(pushableObject))
            return;

        pushObject(pushableObject);
    }

    protected override Vector3 calculateForceDirection(Vector3 objectPosition)
    {
        
        return mover.getMovementDirection();
    }

    protected override float calculateForceAmmount(float distanceToPushableObject)
    {
        float force = pushingStats.maximumPushForce;
        return force;
    }
}
