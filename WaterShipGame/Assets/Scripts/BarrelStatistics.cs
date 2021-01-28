using UnityEngine;

[CreateAssetMenu(fileName = "New Barrel Stats", menuName = "Barrel Statistics")]
public class BarrelStatistics : MovingObjectStatistics
{
    [Header("Explosion Statistics")]
    public float explosionRadius = 5f;
    public float explosionMaximumDamage = 3f;
    public float explosionMinumumDamage = 1f;
}
