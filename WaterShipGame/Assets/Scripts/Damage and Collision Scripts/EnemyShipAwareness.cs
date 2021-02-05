using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAwareness : MonoBehaviour
{
    [SerializeField] private EnemyShipStatistics enemyShipStatistics;
    [SerializeField] private SphereCollider awarenessCollider;

    public event Action playerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            playerDetected?.Invoke();
            enabled = false;
        }
        
    }

    private void Update()
    {
        updateColliderSize();
    }

    private void updateColliderSize()
    {
        if(awarenessCollider.radius != enemyShipStatistics.radiusOfAwareness)
            awarenessCollider.radius = enemyShipStatistics.radiusOfAwareness;
    }
}
