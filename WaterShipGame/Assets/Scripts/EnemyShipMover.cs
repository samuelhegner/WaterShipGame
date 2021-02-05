using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyShipMover : MonoBehaviour, ISleepState
{


    [SerializeField] private float turningSpeed = 5f;
    [SerializeField] private float chargeSpeed = 10f;
    [SerializeField] private float brakeSpeed = 2f;
    [SerializeField] private float accelerationSpeed = 5f;
    [SerializeField] private float distanceToOvershootCharge = 5f;
    [SerializeField] [Range(0, 180)] private float maxTurnAngle;
    [SerializeField] MovementState currentState;


    private enum MovementState
    {
        sleeping,
        turning,
        charging
    }
    

    Rigidbody enemyShipRigidbody;
    Transform playerTransform;

    Coroutine sleepingCoroutine;
    Vector3 lockedPosition;
    Quaternion targetRotation;
    float turnAngle;
    float currentSpeed;


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
            case MovementState.charging:
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
        Vector3 toPlayerDirection = Vector3.Normalize(playerTransform.position - transform.position);
        targetRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(toPlayerDirection, Vector3.up), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxTurnAngle * Time.fixedDeltaTime);
    }

    private void moveShipForward()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, turningSpeed, brakeSpeed * Time.fixedDeltaTime);
        enemyShipRigidbody.MovePosition(transform.position + (transform.forward * currentSpeed * Time.fixedDeltaTime));
    }

    private void checkIfLockPossible()
    {
        if (transform.rotation == targetRotation)
        {
            calculateChargePoint();
            
            currentState = MovementState.charging;
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
        currentSpeed = Mathf.Lerp(currentSpeed, chargeSpeed, accelerationSpeed * Time.fixedDeltaTime);
        enemyShipRigidbody.MovePosition(transform.position + (transform.forward * currentSpeed * Time.fixedDeltaTime));
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
