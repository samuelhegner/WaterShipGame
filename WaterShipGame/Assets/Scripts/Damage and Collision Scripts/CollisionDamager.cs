using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CollisionDamager : MonoBehaviour
{
    [SerializeField] private CollisionDamageStatistics collisionStats;
    public Health HealthOfObject { get => healthOfObject;}
    public CollisionDamageStatistics CollisionStats { get => collisionStats; }
    public event Action<GameObject> onCollisionEvent;

    private Health healthOfObject;


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
            onCollisionEvent?.Invoke(collision.gameObject);
            dealCollisionDamage(collidableObject);
        }
    }

    private void dealCollisionDamage(CollisionDamager collidableObject)
    {
        collidableObject.HealthOfObject.takeDamage(CollisionStats.damageToDealOnCollision);
    }
}
