using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "New Collision Damage Stats", menuName = "Collision Damage Statistics")]
public class CollisionDamageStatistics : ScriptableObject
{
    public bool canTakeDamage;
    public int damageToDealOnCollision;
}
