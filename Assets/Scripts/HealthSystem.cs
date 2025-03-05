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

    /// <summary>
    /// Apply damage to the health system
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject.name} took {damage} damage.");

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
