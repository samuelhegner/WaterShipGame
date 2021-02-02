using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class IncreaseRippleSize : MonoBehaviour
{
    [SerializeField] private RippleStatistics rippleStatistics;
    [SerializeField] private SphereCollider rippleSphereCollider;
    [SerializeField] private Disc rippleVisualDisc;



    float increasingSize = 0f;

    private void Start()
    {
        StartCoroutine(increaseRippleSizeOverTime());
    }

    IEnumerator increaseRippleSizeOverTime()
    {
        while (increasingSize < rippleStatistics.maximumRippleSize) 
        {
            increasingSize = increaseSizeBasedOnRippleStatisticSpeed();
            updateSizeDependantComponents();
            yield return new WaitForEndOfFrame();
        }

        maximumSizeReached();
    }

    private void updateSizeDependantComponents()
    {
        updateColliderSize();
        updateVisualSize();
    }

    private float increaseSizeBasedOnRippleStatisticSpeed()
    {
        return increasingSize + (Time.deltaTime * rippleStatistics.sizeIncreaseSpeed);
    }

    void updateColliderSize()
    {
        rippleSphereCollider.radius = increasingSize;
    }

    void updateVisualSize()
    {
        rippleVisualDisc.Radius = increasingSize;
    }

    private void maximumSizeReached()
    {
        Destroy(gameObject);
    }
}
