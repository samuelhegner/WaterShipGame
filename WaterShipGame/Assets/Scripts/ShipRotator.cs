using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotator : MonoBehaviour
{
    [SerializeField] PlayerShipStatistics shipStatistics;

    Rigidbody objectRigidbody;

    float minSpeedForRotation = 0.1f;

    private void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(canShipRotate())
            rotateShipInMovingDirection();
    }

    private bool canShipRotate()
    {
        return shipStatistics.turnInMovementDirection && objectRigidbody.velocity.magnitude > minSpeedForRotation;
    }

    private void rotateShipInMovingDirection()
    {
        Quaternion targetShipRotation = Quaternion.LookRotation(objectRigidbody.velocity, Vector3.up);
        transform.rotation = lerpRotationToTargetRotation(targetShipRotation);
    }

    private Quaternion lerpRotationToTargetRotation(Quaternion targetShipRotation)
    {
        return Quaternion.Lerp(transform.rotation, targetShipRotation, Time.deltaTime * shipStatistics.turnSpeed);
    }
}
