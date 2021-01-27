using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReflector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PushableObject objectToReflect = collision.transform.GetComponent<PushableObject>();
        if (objectToReflect != null) 
        {
            reflectObject(objectToReflect, collision);
        }
    }

    private void reflectObject(PushableObject pushableObject, Collision collisionInfo)
    {
        pushableObject.bounceObject(collisionInfo);
    }
}
