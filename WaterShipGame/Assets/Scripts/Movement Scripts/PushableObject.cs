using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class PushableObject : MonoBehaviour, IPushable, ISpeedDamper
{
    [SerializeField] private PushableObjectStatistics movementStatistics;
    protected Rigidbody objectRigidbody;

    private void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    public virtual void pushObject(float forceToAdd, Vector3 forceDirection, ForceMode modeToUse)
    {
        Vector3 directionalForce = forceDirection * forceToAdd;

        objectRigidbody.AddForce(directionalForce, modeToUse);
    }

    public bool isKinematic()
    {
        return objectRigidbody.isKinematic;
    }

    public virtual void bounceObject(Plane planeOfReflection)
    {
        if (velocityIsGoingInOposingDirection(planeOfReflection)) {
            float reflectionSpeed = calculateReflectionSpeed();
            Vector3 reflectionDirection = calculateReflectionOffPlane(planeOfReflection);
            objectRigidbody.velocity = reflectionDirection * reflectionSpeed;
        }
    }

    float calculateReflectionSpeed() 
    {
        float speed = objectRigidbody.velocity.magnitude + movementStatistics.minimumBounceSpeed;
        return speed;
    }

    Vector3 calculateReflectionOffPlane(Plane planeOfReflection) 
    {
        Vector3 reflectionDirection = Vector3.Reflect(objectRigidbody.velocity.normalized, planeOfReflection.normal);
        reflectionDirection = Vector3.ProjectOnPlane(reflectionDirection, Vector3.up);
        reflectionDirection = Vector3.Normalize(reflectionDirection);

        return reflectionDirection;
    }

    bool velocityIsGoingInOposingDirection(Plane planeOfReflection) 
    {
        Vector3 pointOfCollisionOnPlane = planeOfReflection.ClosestPointOnPlane(transform.position);
        Vector3 fromColisionPoint = transform.position - pointOfCollisionOnPlane;

        return (Vector3.Dot(fromColisionPoint, objectRigidbody.velocity) < 0);
    }


    public virtual void loseVelocityOverTime()
    {
        if (movementStatistics.velocityDamping != 0)
        {
            objectRigidbody.velocity -= (movementStatistics.velocityDamping * objectRigidbody.velocity * Time.deltaTime);
        }
    }

    public virtual void clampVelocityAtMaximum()
    {
        if (movementStatistics.maximumVelocity > 0)
        {
            objectRigidbody.velocity = Vector3.ClampMagnitude(objectRigidbody.velocity, movementStatistics.maximumVelocity);
        }
    }

    public virtual void Update()
    {
        loseVelocityOverTime();
        clampVelocityAtMaximum();
    }
}
