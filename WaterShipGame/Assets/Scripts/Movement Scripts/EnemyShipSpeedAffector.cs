using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpeedAffector : MonoBehaviour
{
    [SerializeField] private float maxSpeedInfluence;
    [SerializeField] private float minSpeedInfluence;
    [SerializeField] private float speedChange = 10f;


    public event Action<float, Vector3> updateInfluence;

    Vector3 directionOfInfluence;
    float speedOfInfluence;

    float maxDistance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            maxDistance = other.GetComponent<SphereCollider>().radius;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            calculateInfluence(other.transform);
            updateListeners();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            directionOfInfluence = Vector3.zero;
            speedOfInfluence = 0;
            updateListeners();
        }
    }

    private void calculateInfluence(Transform influenceTransform)
    {
        calculateDirectionOfInfluence(influenceTransform);
        calculateSpeedOfInfluence();
    }
    private void calculateDirectionOfInfluence(Transform influencePosition)
    {
        Vector3 tempDir = influencePosition.position - transform.position;
        if (tempDir.magnitude > 0.5f) 
        {
            directionOfInfluence = tempDir;
            directionOfInfluence.y = 0;
        }
        
    }
    private void calculateSpeedOfInfluence()
    {
        float tempSpeed = FloatExtensions.Map(directionOfInfluence.magnitude, 0, maxDistance, maxSpeedInfluence, minSpeedInfluence);

        if (Vector3.Dot(transform.forward, directionOfInfluence) < 0) // is behindinfluence obj
        {
            tempSpeed = -tempSpeed;
        }

        speedOfInfluence = Mathf.Lerp(speedOfInfluence, tempSpeed, speedChange * Time.deltaTime);
    }

    

    
    private void updateListeners()
    {
        updateInfluence?.Invoke(speedOfInfluence, directionOfInfluence.normalized);
    }

}
