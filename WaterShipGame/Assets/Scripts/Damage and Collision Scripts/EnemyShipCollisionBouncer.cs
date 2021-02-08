using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipCollisionBouncer : ObjectPusher
{

    private void OnEnable()
    {
        GetComponent<CollisionDamager>().onCollisionEvent += pushOnCollision;
    }

    private void OnDisable()
    {
        GetComponent<CollisionDamager>().onCollisionEvent -= pushOnCollision;
    }

    void pushOnCollision(GameObject collidingObject) 
    {
        PushableObject pushableObject = collidingObject.GetComponent<PushableObject>();
        if (colliderIsNotPushable(pushableObject))
            return;
        print("Pushed on Collision");
        pushObject(pushableObject);
    }
}
