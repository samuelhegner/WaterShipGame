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

        Vector3 forceDirection = calculateForceDirection(other.transform.position);

        float distanceToPushableObject = Vector3.Distance(other.transform.position, transform.position);

        float forceAmmount = calculateForceAmmount(distanceToPushableObject);

        pushableObject.AddForceToRigidBody(forceAmmount, forceDirection);
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
