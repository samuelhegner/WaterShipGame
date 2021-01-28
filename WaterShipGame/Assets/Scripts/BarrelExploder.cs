using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BarrelExploder : MonoBehaviour
{
    List<IDamageable> closeDamagebleObjects = new List<IDamageable>();
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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void explodeBarrel() 
    {

    }
}
