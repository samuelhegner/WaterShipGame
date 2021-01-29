using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Ripple : MonoBehaviour
{
    [HideInInspector] protected static RippleStatistics rippleStatistics;

    static bool searchedForFile = false;

    string rippleStatisticsPath = "Design Assets/Object Stats/Ripple Statistics";

    private void Awake()
    {
        fillRippleStatisticField();
        testRippleStatisticsExists();
    }

    void fillRippleStatisticField()
    {
        if (!searchedForFile) 
        {
            rippleStatistics = Resources.Load<RippleStatistics>(rippleStatisticsPath);
            searchedForFile = true;
            Debug.Log("Searched for file");
        }
    }

    private void testRippleStatisticsExists()
    {
        Assert.IsNotNull(rippleStatistics, "Ripple Statistics not found at: " + rippleStatisticsPath);
    }
}
