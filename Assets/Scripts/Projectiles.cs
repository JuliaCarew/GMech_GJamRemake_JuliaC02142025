using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    
    [SerializeField] private GameObject woodProjectilePrefab;
    [SerializeField] private GameObject rockProjectilePrefab;
    [SerializeField] private GameObject bottleProjectilePrefab;
    [SerializeField] private GameObject mixedProjectilePrefab;

    [SerializeField] private int damage = 1;

    private void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }
    }

    public void CreateProjectile()
    {
        Debug.Log("Creating projectile...");

        string projectileType = playerInventory.GetProjectileType();

        if (string.IsNullOrEmpty(projectileType)) {
            Debug.Log("No valid Projectile type found");
            return;
        }

        GameObject projectilePrefab = GetProjectilePrefab(projectileType);

        if (projectilePrefab != null)
        {
            Debug.Log("Creating projectile: " + projectileType);
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            playerInventory.RemoveUsedItems();
        }
        else{
            Debug.Log("No valid Projectile prefab found");
        }
    }

    private GameObject GetProjectilePrefab(string projectileType)
    {
        switch (projectileType)
        {
            case "WoodWoodWood":
                Debug.Log("Created WoodWoodWood projectile");
                return woodProjectilePrefab;
            case "RockRockRock":
                Debug.Log("Created RockRockRock projectile");
                return rockProjectilePrefab;
            case "BottleBottleBottle":
                Debug.Log("Created BottleBottleBottle projectile");
                return bottleProjectilePrefab;
            default:
                Debug.Log("Created Mixed projectile");
                return mixedProjectilePrefab;  
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthSystem enemyHealth = other.GetComponent<HealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(3);
            }

            Destroy(gameObject); // Destroy the projectile on impact
        }
    }
}
