using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyShipMover : MonoBehaviour
{
    Rigidbody enemyShipRigidbody;
    Transform playerTransform;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float maxTurnAngle;

    void Start()
    {
        enemyShipRigidbody = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 toPlayer = playerTransform.position - transform.position;
        float angleToPlayer = Vector3.SignedAngle(toPlayer
                                                , transform.forward
                                                , Vector3.down);

        float mappedSpeed = FloatExtensions.Map(Mathf.Abs(angleToPlayer), 0, 180, maxSpeed, minSpeed);

        angleToPlayer = Mathf.Clamp(angleToPlayer, -maxTurnAngle, maxTurnAngle) * Time.deltaTime;

        Vector3 movementDirection = Quaternion.AngleAxis(angleToPlayer, Vector3.up) * transform.forward;

        transform.forward = movementDirection;

        enemyShipRigidbody.MovePosition(transform.position + (transform.forward * mappedSpeed * Time.deltaTime));
    }

    
}
