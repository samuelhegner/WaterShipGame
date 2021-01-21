using System.Collections;
using UnityEngine;

public class SpawnRipple : MonoBehaviour, ITouchable
{
    [SerializeField] private GameObject ripplePrefab;

    
    public void OnTouchDown(Vector3 touchPointInWorldSpace)
    {
        GameObject newRipple = Instantiate(ripplePrefab, touchPointInWorldSpace, Quaternion.identity);
    }

    public void OnTouchHeld(Vector3 touchPointInWorldSpace)
    {
        //throw new System.NotImplementedException();
    }

    public void OnTouchRelease(Vector3 touchPointInWorldSpace)
    {
        //throw new System.NotImplementedException();
    }
}
