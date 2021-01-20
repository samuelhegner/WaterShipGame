using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjectsAway : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (colliderIsNotPushable(other))
            return;
    }

    private bool colliderIsNotPushable(Collider collider)
    {
        collider.GetComponent<>
    }
}
