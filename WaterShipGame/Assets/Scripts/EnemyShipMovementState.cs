using System.Collections;
using UnityEngine;


public abstract class EnemyShipMovementState
{
    protected EnemyShipMover mover;

    protected EnemyShipMovementState(EnemyShipMover newMover) 
    {
        mover = newMover;
    }

    public virtual void updateShip() { }

    public virtual void calculateSpeed() { }
}
