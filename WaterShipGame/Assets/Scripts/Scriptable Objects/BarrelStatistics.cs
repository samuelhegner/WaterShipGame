﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Barrel Stats", menuName = "Barrel Statistics")]
public class BarrelStatistics : PushableObjectStatistics
{
    [Header("Explosion Statistics")]
    public float explosionRadius = 5f;
    public float secondsBeforeExplosion = 0f;
    public float explosionMaximumDamage = 3f;
    public float explosionMinumumDamage = 1f;
}
