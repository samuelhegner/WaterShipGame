using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTileController : MonoBehaviour
{
    [SerializeField] List<Obstacle> tileObstacles;

    private void OnEnable()
    {
        for (int i = 0; i < tileObstacles.Count; i++) 
        {
            tileObstacles[i].obstacleFinished += checkActiveObstacles;
        }

        startTile();
    }

    private void OnDisable()
    {
        for (int i = 0; i < tileObstacles.Count; i++)
        {
            tileObstacles[i].obstacleFinished -= checkActiveObstacles;
        }
        stopTile();
    }

    public void startTile() 
    {
        for (int i = 0; i < tileObstacles.Count; i++)
        {
            tileObstacles[i].StartObstacle();
        }
    }

    public void stopTile() 
    {
        for (int i = 0; i < tileObstacles.Count; i++)
        {
            tileObstacles[i].ResetObstacle();
        }
    }

    public void allObstaclesFinished() 
    {
        stopTile();
    }

    public void checkActiveObstacles() 
    {
        bool obstacleActive = false;

        for (int i = 0; i < tileObstacles.Count; i++)
        {
            if (tileObstacles[i].isActiveObstacle())
                obstacleActive = true;
        }

        if (!obstacleActive)
            allObstaclesFinished();
    }
}
