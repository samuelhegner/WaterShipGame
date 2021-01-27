using System.Collections;
using UnityEngine;


public abstract class PushableObject : MonoBehaviour, IPushable
{
    protected Rigidbody objectRigidbody;

    [SerializeField] ForceMode forceModeToUse;

    [SerializeField] float minimumBounceSpeed = 1f;

    private void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    public virtual void pushObject(float forceToAdd, Vector3 forceDirection)
    {
        Vector3 directionalForce = forceDirection * forceToAdd;

        objectRigidbody.AddForce(directionalForce, forceModeToUse);
    }

    public bool isKinematic()
    {
        return objectRigidbody.isKinematic;
    }

    public virtual void bounceObject(Collision collisionInfo)
    {
        float reflectionSpeed = objectRigidbody.velocity.magnitude + minimumBounceSpeed;
        Vector3 reflectionDirection = Vector3.Reflect(objectRigidbody.velocity.normalized, collisionInfo.contacts[0].normal);
        reflectionDirection.y = 0;
        reflectionDirection = Vector3.Normalize(reflectionDirection);
        objectRigidbody.velocity = reflectionDirection * reflectionSpeed;
    }
}
