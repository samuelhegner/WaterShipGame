using System;
using System.Collections;
using UnityEngine;

public class ChargingState : EnemyShipMovementState
{
    public ChargingState(EnemyShipMover newMover) : base(newMover)
    {
        mover = newMover;
    }

    public override void calculateSpeed()
    {
        mover.CurrentSpeed = Mathf.Lerp(mover.CurrentSpeed
                                , mover.EnemyShipStats.speedWhileCharging + (mover.ExternalSpeedInfluence * mover.ExternalForceChargeMultiplier)
                                , (mover.EnemyShipStats.accelerationSpeed) * Time.fixedDeltaTime);
    }

    public override void updateShip()
    {
        moveShipToRamPoint();
        checkedIfChargePointReached();
    }

    private void checkedIfChargePointReached()
    {
        Vector3 toRamPoint = mover.LockedPosition - mover.transform.position;

        if (toRamPoint.magnitude < 1f)
        {
            mover.setCurrentState(new TurningState(mover));
        }
    }

    private void moveShipToRamPoint()
    {
        mover.EnemyShipRigidbody.MovePosition(mover.transform.position + (mover.transform.forward * mover.CurrentSpeed * Time.fixedDeltaTime));
    }
}
