using System.Collections;
using UnityEngine;


public abstract class PushableObject : MonoBehaviour, IPushable
{
    protected Rigidbody objectRigidbody;

    [SerializeField] ForceMode forceModeToUse;

    private void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    public void AddForceToRigidBody(float forceToAdd, Vector3 forceDirection)
    {
        Vector3 directionalForce = forceDirection * forceToAdd;

        objectRigidbody.AddForce(directionalForce, forceModeToUse);
    }

    public bool isKinematic()
    {
        return objectRigidbody.isKinematic;
    }
}
