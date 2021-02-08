using Shapes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolPuller : ObjectPusher
{
    [Header("Pulling Statistics")]
    [SerializeField] private float pullDistance;

    [Header("Visual Refrences")]
    [SerializeField] private SphereCollider rangeCollider;
    [SerializeField] private Disc rangeRing;


    private void OnTriggerStay(Collider other)
    {
        PushableObject pushableObject = other.GetComponent<PushableObject>();

        if (colliderIsNotPushable(pushableObject))
            return;

        pushObject(pushableObject);
    }

    private void Update()
    {
        updateColliderSize();
        updateRangeRingSize();
    }

    private void updateRangeRingSize()
    {
        if (rangeRing.Radius != pullDistance)
            rangeRing.Radius = pullDistance;
    }

    private void updateColliderSize()
    {
        if (rangeCollider.radius != pullDistance)
            rangeCollider.radius = pullDistance;
    }

    protected override float calculateForceAmmount(float distanceToPushableObject)
    {
        float force = 0;

        force = FloatExtensions.Map(distanceToPushableObject
                                    , 0
                                    , rangeCollider.radius
                                    , pushingStats.maximumPushForce
                                    , pushingStats.minimumPushForce
                                    );
        return force;
    }

    protected override Vector3 calculateForceDirection(Vector3 objectPosition)
    {
        Vector3 direction = transform.position - objectPosition;
        direction = negateYComponent(direction);
        direction = Vector3.Normalize(direction);
        return direction;
    }
}
