﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMover : PushableObject
{
    private void Update()
    {
        loseVelocityOverTime();
    }
}
