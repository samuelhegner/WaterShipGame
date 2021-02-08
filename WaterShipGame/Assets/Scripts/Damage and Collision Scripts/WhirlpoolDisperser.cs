using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolDisperser : MonoBehaviour, ITouchable
{
    [SerializeField] private GameObject parent;
    [SerializeField] private int numberOfTouchesToDisperse;

    int touchCount = 0;
    public void OnTouchDown(Vector3 touchPointInWorldSpace)
    {
    }

    public void OnTouchHeld(Vector3 touchPointInWorldSpace)
    {

    }

    public void OnTouchRelease(Vector3 touchPointInWorldSpace)
    {
        touchCount++;
        if (dispersable()) 
        {
            disperse();
        }
    }

    private bool dispersable()
    {
        return touchCount >= numberOfTouchesToDisperse;
    }

    private void disperse()
    {
        Destroy(parent);
    }
}
