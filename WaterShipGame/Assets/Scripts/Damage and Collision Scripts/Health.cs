using System.Collections;
using UnityEngine;
using System;


public class Health : MonoBehaviour, IDamageable
{
    [SerializeField][Range(1, 10)] int startingHealth = 1;
    float currentHealth;

    public Action<float> currentHealthChanged;

    public Action objectDestroyed;

    void Awake()
    {
        currentHealth = (float)startingHealth;
    }


    public void healDamage(float damageToHeal)
    {
        currentHealth += damageToHeal;
        updateHealthListeners();
    }

    public void takeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        updateHealthListeners();
    }

    void updateHealthListeners() 
    {
        currentHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0) 
        {
            objectDestroyed?.Invoke();
        }
    }
}
