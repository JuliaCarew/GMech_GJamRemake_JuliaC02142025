using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private int damageToPlayer = 1;

    private HealthSystem healthSystem;
    [SerializeField] private GameObject hitEffectPrefab;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        healthSystem = GetComponent<HealthSystem>();

        if (healthSystem == null)
        {
            Debug.LogError("Enemy has no HealthSystem attached!");
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    /// <summary>
    /// Handle collision with the player or a projectile
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy collided with: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!");
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
                Debug.Log("Player took " + damageToPlayer + " damage.");
            }
        }
        else if (other.CompareTag("Projectile"))
        {
            Debug.Log("Enemy hit by a projectile!");

            // Apply damage to the enemy from the projectile
            Projectiles projectile = other.GetComponent<Projectiles>();
            int damageAmount = projectile != null ? projectile.GetDamage() : 1;

            healthSystem.TakeDamage(damageAmount);

            // Show hit effect above the enemy's head
            ShowHitEffect();

            Destroy(other.gameObject);

            if (healthSystem.CurrentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the enemy
            }
        }
    }

    /// <summary>
    /// Show a hit effect above the enemy's head
    /// </summary>
    private void ShowHitEffect()
    {
        if (hitEffectPrefab != null)
        {
            // Instantiate the hit effect above the enemy's head
            Vector3 effectPosition = transform.position + new Vector3(0, 0.1f, 0); // offset
            GameObject hitEffect = Instantiate(hitEffectPrefab, effectPosition, Quaternion.identity);

            Destroy(hitEffect, 0.5f);
        }
    }
}