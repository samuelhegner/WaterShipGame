﻿using System.Collections;
using UnityEngine;

public interface IPushable
{
    bool isKinematic();

    void pushObject(float forceToAdd, Vector3 forceDirection, ForceMode modeToUse);

    void bounceObject(Plane planeOfRefection);
    
}
