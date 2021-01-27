using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : PushableObject
{
    [SerializeField][Range(0,1)] float velocityDamping = 0;
    private void Update()
    {
        // Todo: fix this function to work properly
        
        loseVelocityOverTime();
    }

    private void loseVelocityOverTime()
    {
        if (velocityDamping != 0) 
        {
            //float percentageMultiplicationAmmount = ((100f - percentageOfVelocityLost) / 100f);
            

            objectRigidbody.velocity -= (velocityDamping * objectRigidbody.velocity * Time.deltaTime);

        }
    }
}
