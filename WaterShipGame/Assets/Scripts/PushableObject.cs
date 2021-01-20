using System.Collections;
using UnityEngine;


public abstract class PushableObject : MonoBehaviour, IPushable
{
    [SerializeField] ForceMode forceModeToUse;

    Vector3 pushForce;

    Rigidbody rigidbody;
    
    public void addToPushForce(Vector3 pushForceToAdd)
    {
        pushForce += pushForceToAdd;
    }


    void Update() 
    {
        
    }
}
