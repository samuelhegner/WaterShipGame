using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CollisionDamager : MonoBehaviour
{
    [SerializeField] private CollisionDamageStatistics collisionStats;
    private Health healthOfObject;

    public Health HealthOfObject { get => healthOfObject;}
    public CollisionDamageStatistics CollisionStats { get => collisionStats; }

    private void Start()
    {
        if (CollisionStats.canTakeDamage)
        {
            healthOfObject = GetComponent<Health>();
            Assert.IsNotNull(healthOfObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionDamager collidableObject = collision.transform.GetComponent<CollisionDamager>();

        if (collidableObject != null && collidableObject.CollisionStats.canTakeDamage)
        {
            dealCollisionDamage(collidableObject);
        }
    }

    private void dealCollisionDamage(CollisionDamager collidableObject)
    {
        collidableObject.HealthOfObject.takeDamage(CollisionStats.damageToDealOnCollision);
    }
}
