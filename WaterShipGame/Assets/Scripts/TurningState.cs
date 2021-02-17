using System;
using System.Collections;
using UnityEngine;


public class TurningState : EnemyShipMovementState
{
    public TurningState(EnemyShipMover newMover) : base(newMover)
    {
        mover = newMover;
    }

    Quaternion targetRotation;

    public override void calculateSpeed()
    {
        mover.CurrentSpeed = Mathf.Lerp(mover.CurrentSpeed
                                    , mover.EnemyShipStats.speedWhileTurning + mover.ExternalSpeedInfluence
                                    , (mover.EnemyShipStats.brakeSpeed) * Time.fixedDeltaTime);
    }

    public override void updateShip()
    {
        turnShip();
        moveShipForward();
        checkIfLockPossible();
    }

    private void turnShip()
    {
        Vector3 toPlayerDirection = Vector3.Normalize(mover.PlayerTransform.position - mover.transform.position);
        targetRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(toPlayerDirection, Vector3.up), Vector3.up);
        mover.transform.rotation = Quaternion.RotateTowards(mover.transform.rotation, targetRotation, mover.EnemyShipStats.turnSpeed * Time.fixedDeltaTime);
    }

    private void moveShipForward()
    {
        mover.EnemyShipRigidbody.MovePosition(mover.transform.position + (mover.transform.forward * mover.CurrentSpeed * Time.fixedDeltaTime));
    }


    private void checkIfLockPossible()
    {
        if (mover.transform.rotation == targetRotation)
        {
            calculateChargePoint();
            faceChargeDirection();
            mover.setCurrentState(new ChargingState(mover));
        }
    }

    private void calculateChargePoint()
    {
        Vector3 toPointDir = Vector3.Normalize(mover.PlayerTransform.position - mover.transform.position);

        Vector3 pointToCharge = mover.PlayerTransform.position + (toPointDir * mover.EnemyShipStats.distanceToOvershootCharge);

        mover.LockedPosition = pointToCharge;
    }

    private void faceChargeDirection()
    {
        Vector3 directionToRamPoint = Vector3.Normalize(mover.LockedPosition - mover.transform.position);
        mover.transform.forward = directionToRamPoint;
    }
}
