using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : PushableObject
{
    [SerializeField][Range(0,100)] float percentageOfVelocityLost = 0;
    private void Update()
    {
        // Todo: fix this function to work properly
        
        //loseVelocityOverTime();
    }

    private void loseVelocityOverTime()
    {
        if (percentageOfVelocityLost != 0) 
        {
            float percentageMultiplicationAmmount = ((100f - percentageOfVelocityLost) / 100f);
            objectRigidbody.velocity = objectRigidbody.velocity * percentageMultiplicationAmmount * Time.deltaTime;

        }
    }
}
