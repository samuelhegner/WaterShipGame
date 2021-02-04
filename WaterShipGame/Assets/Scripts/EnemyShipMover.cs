using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyShipMover : MonoBehaviour, ISleepState
{


    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] [Range(0, 1)] private float turningSpeedLossMultiplier = 0.5f;
    [SerializeField] private float distanceToOvershootCharge = 5f;
    [SerializeField] float minTurnSpeed = 5f;
    [SerializeField] float maxTurnSpeed = 10f;
    [SerializeField] [Range(0, 180)] private float maxTurnAngle;


    enum MovementState
    {
        sleeping,
        turning,
        lockedIn
    }
    
    [SerializeField] MovementState currentState;

    Rigidbody enemyShipRigidbody;
    Transform playerTransform;

    Vector3 lockedPosition;
    Coroutine sleepingCoroutine;
    
    float turnAngle;


    void Start()
    {
        enemyShipRigidbody = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = MovementState.sleeping;
        sleepForSeconds(2f);
    }

    void FixedUpdate()
    {
        updateShip();
    }


    private void updateShip()
    {
        switch (currentState)
        {
            case MovementState.sleeping:
                {
                    break;
                }
            case MovementState.turning:
                {
                    turnShip();
                    moveShipForward();
                    checkIfLockPossible();
                    break;
                }
            case MovementState.lockedIn:
                {
                    moveShipToRamPoint();
                    checkedIfChargePointReached();
                    break;
                }
            default:
                break;
        }
    }

    private void turnShip()
    {
        turnAngle = calculateSignedTurnAngle();
        transform.forward = turnForwardToNewDirection(turnAngle);
    }

    private float calculateSignedTurnAngle()
    {
        Vector3 toPlayer = playerTransform.position - transform.position;
        float angleToPlayer = Vector3.SignedAngle(toPlayer
                                                 , transform.forward
                                                 , Vector3.down);
        return angleToPlayer;
    }

    private Vector3 turnForwardToNewDirection(float angleToTurn)
    {
        float modifiedTurnAngle = turnAngle;
        float mappedTurnSpeed = FloatExtensions.Map(Math.Abs(modifiedTurnAngle), 0, 180, maxTurnSpeed, minTurnSpeed);

        modifiedTurnAngle = modifiedTurnAngle * (mappedTurnSpeed * Time.fixedDeltaTime);
        Vector3 movementDirection = Quaternion.AngleAxis(modifiedTurnAngle, Vector3.up) * transform.forward;
        return movementDirection;
    }

    private void moveShipForward()
    {
        float mappedSpeed = FloatExtensions.Map(Mathf.Abs(turnAngle), 0, 180, maxSpeed * turningSpeedLossMultiplier, minSpeed);
        enemyShipRigidbody.MovePosition(transform.position + (transform.forward * mappedSpeed * Time.fixedDeltaTime));
    }

    private void checkIfLockPossible()
    {
        if (Mathf.Abs(turnAngle) < 1f)
        {
            calculateChargePoint();
            
            currentState = MovementState.lockedIn;
            faceChargeDirection();
        }
    }

    private void calculateChargePoint()
    {
        Vector3 toPointDir = Vector3.Normalize(playerTransform.position - transform.position);

        Vector3 pointToCharge = playerTransform.position + (toPointDir * distanceToOvershootCharge);

        lockedPosition = pointToCharge;
    }

    private void faceChargeDirection()
    {
        Vector3 directionToRamPoint = Vector3.Normalize(lockedPosition - transform.position);
        transform.forward = directionToRamPoint;
    }

    private void moveShipToRamPoint()
    {
        enemyShipRigidbody.MovePosition(transform.position + (transform.forward * maxSpeed * Time.fixedDeltaTime));
    }

    private void checkedIfChargePointReached()
    {
        Vector3 toRamPoint = lockedPosition - transform.position;
        if (toRamPoint.magnitude < 1f) 
        {
            currentState = MovementState.turning;
        }
    }

    public bool isSleeping()
    {
        return currentState == MovementState.sleeping;
    }

    public void enableSleeping()
    {
        if (sleepingCoroutine != null)
            StopCoroutine(sleepingCoroutine);
        currentState = MovementState.sleeping;
    }

    public void disableSleeping()
    {
        if (sleepingCoroutine != null)
            StopCoroutine(sleepingCoroutine);
        currentState = MovementState.turning;
    }

    public void sleepForSeconds(float secondsToSleepFor)
    {
        sleepingCoroutine = StartCoroutine(sleepTimer(secondsToSleepFor));
    }

    IEnumerator sleepTimer(float secondsToSleepFor)
    {
        currentState = MovementState.sleeping;
        yield return new WaitForSeconds(secondsToSleepFor);
        currentState = MovementState.turning;
    }
}
