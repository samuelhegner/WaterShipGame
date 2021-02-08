using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class EnemyShipAwareness : MonoBehaviour
{
    [Header("Awareness Statistics")]
    [SerializeField] private EnemyShipStatistics enemyShipStatistics;

    [Header("Visual Refrences")]
    [SerializeField] private SphereCollider awarenessCollider;
    [SerializeField] private Disc rangeRing;

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
        updateRangeRingSize();
    }

    private void updateRangeRingSize()
    {
        if (rangeRing.Radius != enemyShipStatistics.radiusOfAwareness)
            rangeRing.Radius = enemyShipStatistics.radiusOfAwareness;
    }

    private void updateColliderSize()
    {
        if(awarenessCollider.radius != enemyShipStatistics.radiusOfAwareness)
            awarenessCollider.radius = enemyShipStatistics.radiusOfAwareness;
    }
}
