using System.Collections;
using UnityEngine;


public abstract class ObjectPusher : MonoBehaviour
{
    [SerializeField] protected PushingStatistics pushingStats;

    protected virtual void pushObject(PushableObject pushableObject)
    {
        Vector3 forceDirection = calculateForceDirection(pushableObject.transform.position);

        float distanceToPushableObject = Vector3.Distance(pushableObject.transform.position, transform.position);

        float forceAmmount = calculateForceAmmount(distanceToPushableObject);

        pushableObject.pushObject(forceAmmount, forceDirection);
    }

    protected virtual Vector3 calculateForceDirection(Vector3 objectPosition)
    {
        Vector3 direction = objectPosition - transform.position;
        direction = negateYComponent(direction);
        direction = Vector3.Normalize(direction);
        return direction;
    }

    protected virtual Vector3 negateYComponent(Vector3 vectorToNegate)
    {
        vectorToNegate.y = 0;
        return vectorToNegate;
    }

    protected virtual float calculateForceAmmount(float distanceToPushableObject)
    {
        float force = 0;

        force = FloatExtensions.Map(distanceToPushableObject
                                    , 0
                                    , 100
                                    , pushingStats.maximumPushForce
                                    , pushingStats.minimumPushForce
                                    );
        return force;
    }

    protected bool colliderIsNotPushable(IPushable pushableObject)
    {
        return pushableObject == null;
    }
}
