using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishMover : PushableObject
{
    [SerializeField] JellyfishStatistics jellyfishStats;
    [SerializeField] private Transform[] waypoints;


    Transform currentWaypoint;
    int waypointIndex = 0;
    Vector3 toWaypoint;

    void Start()
    {
        setCurrentWaypoint();
    }

    

    void FixedUpdate()
    {
        calculateToWaypoint();
        moveToCurrentWaypoint();
        checkIfWaypointReached();
    }

    private void calculateToWaypoint()
    {
        toWaypoint = currentWaypoint.position - transform.position;
    }

    private void moveToCurrentWaypoint()
    {
        if (objectRigidbody.velocity.magnitude < 0.1f)
        {
            Vector3 directionOfMovement = toWaypoint;
            directionOfMovement.y = 0;
            objectRigidbody.MovePosition(transform.position + (jellyfishStats.movementSpeed * directionOfMovement.normalized * Time.fixedDeltaTime));
        }
        
    }
    private void checkIfWaypointReached()
    {
        if (toWaypoint.magnitude <= jellyfishStats.distanceBeforeWaypointReached) 
        {
            setCurrentWaypoint();
        }
    }
    private void setCurrentWaypoint()
    {
        currentWaypoint = waypoints[waypointIndex];
        setNextWaypointIndex();
    }

    private void setNextWaypointIndex()
    {
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }

    private void OnDrawGizoms()
    {
        /*if (isActiveAndEnabled)
        {
            Gizmos.color = new Color(1, 0, 0, 1);
            Gizmos.DrawWireSphere(currentWaypoint.position, 2);
        }*/
        
    }
}
