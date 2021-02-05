using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleObjectPusher : ObjectPusher
{
    [SerializeField] private RippleStatistics rippleStatistics;
    
    private void OnTriggerEnter(Collider other)
    {
        PushableObject pushableObject = other.GetComponent<PushableObject>();

        if (colliderIsNotPushable(pushableObject))
            return;

        pushObject(pushableObject);
    }

    protected override float calculateForceAmmount(float distanceToPushableObject)
    {
        float force = 0;

        force = FloatExtensions.Map(distanceToPushableObject
                                    , 0
                                    , rippleStatistics.maximumRippleSize
                                    , pushingStats.maximumPushForce
                                    , pushingStats.minimumPushForce
                                    );
        return force;
    }
}
