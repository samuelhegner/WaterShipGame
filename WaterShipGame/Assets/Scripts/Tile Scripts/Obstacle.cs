using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Obstacle: MonoBehaviour
{
    public event Action obstacleFinished;

    List<ITileObject> obstacleComponents;

    bool activeObstacle = false;

    private void Awake()
    {
        obstacleComponents = InterfaceHelper.FindInterfacesOfType<ITileObject>(gameObject);
    }

    public void ResetObstacle() 
    {
        for (int i = 0; i < obstacleComponents.Count; i++)
        {
            obstacleComponents[i].reset();
            obstacleComponents[i].fallAsleep();
        }
    }

    public void StartObstacle() 
    {
        activeObstacle = true;

        for (int i = 0; i < obstacleComponents.Count; i++)
        {
            obstacleComponents[i].wakeUp();
        }
    }

    public void onObstacleFinished() 
    {
        activeObstacle = false;
        ResetObstacle();
        obstacleFinished?.Invoke();
    }

    public bool isActiveObstacle() 
    {
        return activeObstacle;
    }
}