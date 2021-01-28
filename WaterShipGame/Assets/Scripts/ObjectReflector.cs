using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReflector : MonoBehaviour
{
    Plane planeOfReflection;

    Queue<PushableObject> recentlyBounced = new Queue<PushableObject>();

    private void Start()
    {
        planeOfReflection = new Plane(transform.forward, transform.position);
        StartCoroutine(clearBouncedObjects());
    }

    private void OnTriggerEnter(Collider other)
    {
        PushableObject objectToReflect = other.transform.GetComponent<PushableObject>();
        if (objectToReflect != null && !recentlyBounced.Contains(objectToReflect))
        {
            recentlyBounced.Enqueue(objectToReflect);
            reflectObject(objectToReflect);
        }
    }

    

    private void reflectObject(PushableObject pushableObject)
    {
        pushableObject.bounceObject(planeOfReflection);
    }

    IEnumerator clearBouncedObjects() 
    {
        while (true) 
        {
            if (!recentBounceQueueIsEmpty()) 
            {
                recentlyBounced.Dequeue();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    bool recentBounceQueueIsEmpty() 
    {
        return recentlyBounced.Count == 0;
    }
}
