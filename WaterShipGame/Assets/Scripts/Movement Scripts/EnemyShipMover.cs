using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyShipMover : MonoBehaviour, ISleepState
{
    [SerializeField] private EnemyShipStatistics enemyShipStats;
    [SerializeField] private MovementState currentState = MovementState.sleeping; //Serialized field for debugging purposes
    [SerializeField] private float secondsToSleepAfterCollision = 1f;

    internal void sleepForSeconds(object stunDuration)
    {
        throw new NotImplementedException();
    }

    [SerializeField] private float externalForceChargeMultiplier = 2f;



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
    float currentSpeed;
    [SerializeField] private float externalSpeedInfluence;
    [SerializeField] private Vector3 externalDirectionInfluence;

    void Start()
    {
        enemyShipRigidbody = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        updateShip();
    }

    

    private void updateShip()
    {
        calculateCurrentSpeed();

        switch (currentState)
        {
            case MovementState.sleeping:
                {
                    sitIdle();
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

    private void sitIdle()
    {
        if (externalSpeedInfluence != 0) 
        {
            enemyShipRigidbody.MovePosition(transform.position + (externalDirectionInfluence * currentSpeed * Time.fixedDeltaTime));
            targetRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(externalDirectionInfluence, Vector3.up), Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime);
        }
    }

    private void calculateCurrentSpeed() 
    {
        if (currentState == MovementState.charging)
        {
            currentSpeed = Mathf.Lerp(currentSpeed
                                    , enemyShipStats.speedWhileCharging + (externalSpeedInfluence * externalForceChargeMultiplier)
                                    , (enemyShipStats.accelerationSpeed) * Time.fixedDeltaTime);
        }
        else if (currentState == MovementState.turning)
        {
            currentSpeed = Mathf.Lerp(currentSpeed
                                    , enemyShipStats.speedWhileTurning + externalSpeedInfluence
                                    , (enemyShipStats.brakeSpeed) * Time.fixedDeltaTime);
        }
        else 
        {
            currentSpeed = externalSpeedInfluence;
        }
    }

    private void turnShip()
    {
        Vector3 toPlayerDirection = Vector3.Normalize(playerTransform.position - transform.position);
        targetRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(toPlayerDirection, Vector3.up), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, enemyShipStats.turnSpeed * Time.fixedDeltaTime);
    }

    private void moveShipForward()
    {
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

        Vector3 pointToCharge = playerTransform.position + (toPointDir * enemyShipStats.distanceToOvershootCharge);

        lockedPosition = pointToCharge;
    }

    private void faceChargeDirection()
    {
        Vector3 directionToRamPoint = Vector3.Normalize(lockedPosition - transform.position);
        transform.forward = directionToRamPoint;
    }

    private void moveShipToRamPoint()
    {
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

    private void OnEnable()
    {
        GetComponent<EnemyShipAwareness>().playerDetected += playerWasDetected;
        GetComponent<CollisionDamager>().onCollisionEvent += sleepAfterCollision;
        GetComponent<EnemyShipSpeedAffector>().updateInfluence += updateExternalForces;
    }

    private void updateExternalForces(float speed, Vector3 direction)
    {
        externalDirectionInfluence = direction;
        externalSpeedInfluence = speed;
    }

    private void OnDisable()
    {
        GetComponent<EnemyShipAwareness>().playerDetected -= playerWasDetected;
        GetComponent<CollisionDamager>().onCollisionEvent -= sleepAfterCollision;
        GetComponent<EnemyShipSpeedAffector>().updateInfluence -= updateExternalForces;
    }

    void sleepAfterCollision(GameObject collidingObject) 
    {
        sleepForSeconds(secondsToSleepAfterCollision);
    }

    void playerWasDetected() 
    {
        currentState = MovementState.turning;
    }

}
