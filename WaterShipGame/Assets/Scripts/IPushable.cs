using System.Collections;
using UnityEngine;

public interface IPushable
{
    bool isKinematic();

    void AddForceToRigidBody(float forceToAdd, Vector3 forceDirection);
}
