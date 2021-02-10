using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishStunner : MonoBehaviour
{
    [SerializeField] private JellyfishStatistics jellyfishStats;
    private void OnTriggerEnter(Collider other)
    {
        int layerHit = other.gameObject.layer;
        
        if (layerHit == 9) //Player Layer
        {
            print("Player Stunned!");
            other.GetComponent<ShipMover>().stunForSeconds(jellyfishStats.stunDuration);
        }
        else if(layerHit == 12)//Enemy Ships 
        {
            print("Enemy Ship Stunned!");
            other.transform.root.GetComponent<EnemyShipMover>().sleepForSeconds(jellyfishStats.stunDuration);
        }

    }
}
