using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMover : MonoBehaviour
{
    [Header("Wave Movement Stats")]
    [SerializeField] private Transform endLocation;
    [SerializeField] private float movementSpeed;
    [SerializeField] private AnimationCurve speedOverTime;

    public event Action reachedEndLocation;

    Vector3 endLocationPoint;
    float maxDistanceToMove;


    
    void Start()
    {
        maxDistanceToMove = (endLocation.position - transform.position).magnitude;
        endLocationPoint = endLocation.position;
    }

    // Update is called once per frame
    void Update()
    {
        float progressToEndLocation = calculateProgressToEndLocation();

        if (!endPointReached(progressToEndLocation))
        {
            float calculatedSpeed = calculateSpeedWithCurve(progressToEndLocation);
            transform.position = Vector3.MoveTowards(transform.position
                                                 , endLocationPoint
                                                 , calculatedSpeed * Time.deltaTime);
        }
        else 
        {
            reachedEndLocation?.Invoke();
            enabled = false;
        }
    }

    private bool endPointReached(float progressToEndLocation)
    {
        return progressToEndLocation >= 0.99f;
    }

    private float calculateProgressToEndLocation()
    {
        float distanceToEnd = (endLocationPoint - transform.position).magnitude;

        float progressToEnd = 0;

        float distanceCovered = maxDistanceToMove - distanceToEnd;

        if (distanceCovered != 0)
        {
            progressToEnd = 1f/ (maxDistanceToMove / distanceCovered); //returns 0-1 to evaluate speedOverTimeCurve
        }

        return progressToEnd;
    }

    private float calculateSpeedWithCurve(float curveEvaluationTime)
    {
        return movementSpeed * speedOverTime.Evaluate(curveEvaluationTime);
    }

    
}
