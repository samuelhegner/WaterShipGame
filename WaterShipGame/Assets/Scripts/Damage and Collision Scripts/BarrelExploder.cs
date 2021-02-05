using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class BarrelExploder : MonoBehaviour, IDestroyable
{
    [SerializeField] private BarrelStatistics barrelStats;

    List<Health> damageableObjects = new List<Health>();

    float[] distanceToObjects;

    void explodeBarrel() 
    {
        StartCoroutine(explodeBarrelCoroutine());
    }


    IEnumerator explodeBarrelCoroutine() 
    {
        yield return new WaitForSeconds(barrelStats.secondsBeforeExplosion);
        Debug.Log("BOOOOOOOOOOOOOM!");
        damageCloseObjects(damageableObjects);
    }

    private void damageCloseObjects(List<Health> damageableObjects)
    {

        getListOfDamageableObjects();
        calculateDistanceToObjects();

        for (int i = 0; i < damageableObjects.Count; i++) 
        {
            if (distanceToObjects[i] <= barrelStats.explosionRadius) 
            {
                float damageToDeal = calculateDamageBasedOnDistance(distanceToObjects[i]);
                damageableObjects[i].takeDamage(damageToDeal);
            }
        }
        onDestroy();
    }

    private void getListOfDamageableObjects()
    {
        damageableObjects = FindObjectsOfType<Health>().ToList();
    }

    private void calculateDistanceToObjects()
    {
        distanceToObjects = new float[damageableObjects.Count];
        for (int i = 0; i < damageableObjects.Count; i++)
        {
            distanceToObjects[i] = Vector3.Distance(transform.position, damageableObjects[i].transform.position);
        }
    }

    private float calculateDamageBasedOnDistance(float distance)
    {
        float damageMappedToDistance = FloatExtensions.Map(distance
                                                           , 0
                                                           , barrelStats.explosionRadius
                                                           , barrelStats.explosionMaximumDamage
                                                           , barrelStats.explosionMinumumDamage);

        return damageMappedToDistance;
    }

    private void OnEnable()
    {
        Health health = GetComponent<Health>();
        Assert.IsNotNull(health);
        health.objectDestroyed += explodeBarrel;
    }
    private void OnDisable()
    {
        Health health = GetComponent<Health>();
        Assert.IsNotNull(health);
        health.objectDestroyed -= explodeBarrel;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, barrelStats.explosionRadius);
    }

    public void onDestroy()
    {
        Destroy(gameObject);
    }
}
