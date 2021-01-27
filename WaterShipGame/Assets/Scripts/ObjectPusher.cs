using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPusher : Ripple
{
    private void OnTriggerEnter(Collider other)
    {
        PushableObject pushableObject = other.GetComponent<PushableObject>();

        if (colliderIsNotPushable(pushableObject))
            return;

        pushObject(pushableObject);
    }

    private void pushObject(PushableObject pushableObject)
    {
        Vector3 forceDirection = calculateForceDirection(pushableObject.transform.position);

        float distanceToPushableObject = Vector3.Distance(pushableObject.transform.position, transform.position);

        float forceAmmount = calculateForceAmmount(distanceToPushableObject);

        pushableObject.pushObject(forceAmmount, forceDirection);
    }

    private bool colliderIsNotPushable(IPushable pushableObject)
    {
        return pushableObject == null;
    }

    private Vector3 calculateForceDirection(Vector3 objectPosition)
    {
        Vector3 direction = objectPosition - transform.position;
        direction = negateYComponent(direction);
        direction = Vector3.Normalize(direction);
        return direction;
    }

    private Vector3 negateYComponent(Vector3 vectorToNegate)
    {
        vectorToNegate.y = 0;
        return vectorToNegate;
    }

    private float calculateForceAmmount(float distanceToPushableObject)
    {
        float force = 0;

        force = FloatExtensions.Map(distanceToPushableObject
                                    , 0
                                    , rippleStatistics.maximumRippleSize
                                    , rippleStatistics.maximumPushForce
                                    , rippleStatistics.minimumPushForce
                                    );
        return force;
    }


}
