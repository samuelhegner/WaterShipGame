using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishMover : PushableObject, ITileObject
{
    [SerializeField] JellyfishStatistics jellyfishStats;
    [SerializeField] private Transform[] waypoints;


    Transform currentWaypoint;
    [SerializeField] int waypointIndex = 0;
    Vector3 toWaypoint;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
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

    public void wakeUp()
    {
        enabled = true;
    }

    public void fallAsleep()
    {
        enabled = false;
    }

    public void reset()
    {
        transform.position = startPosition;
        objectRigidbody.velocity = Vector3.zero;
        waypointIndex = 0;
        currentWaypoint = waypoints[waypointIndex];
    }
}
