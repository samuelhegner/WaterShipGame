using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyShipMover : MonoBehaviour, ISleepState
{
    [SerializeField] private EnemyShipStatistics enemyShipStats;
    [SerializeField] private float secondsToSleepAfterCollision = 1f;
    [SerializeField] private float externalSpeedInfluence;
    [SerializeField] private Vector3 externalDirectionInfluence;
    [SerializeField] private float externalForceChargeMultiplier = 2f;



    Rigidbody enemyShipRigidbody;
    EnemyShipMovementState currentState;
    Transform playerTransform;
    Coroutine sleepingCoroutine;
    Vector3 lockedPosition;
    float currentSpeed;

    public Rigidbody EnemyShipRigidbody { get => enemyShipRigidbody; set => enemyShipRigidbody = value; }
    public float ExternalSpeedInfluence { get => externalSpeedInfluence; set => externalSpeedInfluence = value; }
    public Vector3 ExternalDirectionInfluence { get => externalDirectionInfluence; set => externalDirectionInfluence = value; }
    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public EnemyShipStatistics EnemyShipStats { get => enemyShipStats; set => enemyShipStats = value; }
    public float ExternalForceChargeMultiplier { get => externalForceChargeMultiplier; set => externalForceChargeMultiplier = value; }
    public Transform PlayerTransform { get => playerTransform; set => playerTransform = value; }
    public Vector3 LockedPosition { get => lockedPosition; set => lockedPosition = value; }

    void Start()
    {
        enemyShipRigidbody = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        setCurrentState(new SleepingState(this));
        print(currentState);
    }

    void FixedUpdate()
    {
        updateShip();
    }



    private void updateShip()
    {
        currentState.calculateSpeed();
        currentState.updateShip();
    }



    public bool isSleeping()
    {
        return currentState is SleepingState;
    }

    public void enableSleeping()
    {
        if (sleepingCoroutine != null)
            StopCoroutine(sleepingCoroutine);
        setCurrentState(new SleepingState(this));
    }

    public void disableSleeping()
    {
        if (sleepingCoroutine != null)
            StopCoroutine(sleepingCoroutine);
        setCurrentState(new TurningState(this));
    }

    public void sleepForSeconds(float secondsToSleepFor)
    {
        sleepingCoroutine = StartCoroutine(sleepTimer(secondsToSleepFor));
    }

    IEnumerator sleepTimer(float secondsToSleepFor)
    {
        setCurrentState(new SleepingState(this));
        yield return new WaitForSeconds(secondsToSleepFor);
        setCurrentState(new TurningState(this));
    }

    void sleepAfterCollision(GameObject collidingObject)
    {
        sleepForSeconds(secondsToSleepAfterCollision);
    }

    void playerWasDetected()
    {
        setCurrentState(new TurningState(this));
    }

    private void updateExternalForces(float speed, Vector3 direction)
    {
        externalDirectionInfluence = direction;
        externalSpeedInfluence = speed;
    }

    public void setCurrentState(EnemyShipMovementState newState)
    {
        currentState = newState;
    }


    private void OnEnable()
    {
        GetComponent<EnemyShipAwareness>().playerDetected += playerWasDetected;
        GetComponent<CollisionDamager>().onCollisionEvent += sleepAfterCollision;
        GetComponent<EnemyShipSpeedAffector>().updateInfluence += updateExternalForces;
    }

    private void OnDisable()
    {
        GetComponent<EnemyShipAwareness>().playerDetected -= playerWasDetected;
        GetComponent<CollisionDamager>().onCollisionEvent -= sleepAfterCollision;
        GetComponent<EnemyShipSpeedAffector>().updateInfluence -= updateExternalForces;
    }
}
