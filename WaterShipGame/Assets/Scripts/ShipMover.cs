﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : PushableObject
{
    private void Update()
    {        
        loseVelocityOverTime();
    }
}
