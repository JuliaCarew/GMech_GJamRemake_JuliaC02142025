using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            OnHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
