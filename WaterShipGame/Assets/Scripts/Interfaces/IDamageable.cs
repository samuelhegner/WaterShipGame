using System;
using UnityEngine;
public interface IDamageable
{
    void takeDamage(float damageToTake);
    void healDamage(float damageToHeal);
}