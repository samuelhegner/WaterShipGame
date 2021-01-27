using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : PushableObject
{
    [SerializeField][Range(0,1)] float velocityDamping = 0.2f;
    private void Update()
    {        
        loseVelocityOverTime();
    }

    private void loseVelocityOverTime()
    {
        if (velocityDamping != 0) 
        {            
            objectRigidbody.velocity -= (velocityDamping * objectRigidbody.velocity * Time.deltaTime);
        }
    }
}
