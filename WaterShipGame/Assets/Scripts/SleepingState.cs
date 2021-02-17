using System;
using System.Collections;
using UnityEngine;


public class SleepingState : EnemyShipMovementState
{
    public SleepingState(EnemyShipMover newMover) : base(newMover)
    {
        mover = newMover;
    }

    Quaternion targetRotation;

    public override void updateShip()
    {
        sitIdle();
    }

    public override void calculateSpeed()
    {
        mover.CurrentSpeed = mover.ExternalSpeedInfluence;
    }

    private void sitIdle()
    {
        if (mover.ExternalSpeedInfluence != 0)
        {
            mover.EnemyShipRigidbody.MovePosition(mover.transform.position 
                                                + (mover.ExternalDirectionInfluence 
                                                    * mover.CurrentSpeed 
                                                    * Time.fixedDeltaTime));
            
            targetRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(mover.ExternalDirectionInfluence, Vector3.up)
                                                   , Vector3.up);

            mover.transform.rotation = Quaternion.Slerp(mover.transform.rotation
                                                , targetRotation
                                                , Time.fixedDeltaTime);
        }
    }
}
