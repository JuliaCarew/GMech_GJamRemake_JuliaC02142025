using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    
    [SerializeField] private GameObject woodProjectilePrefab;
    [SerializeField] private GameObject rockProjectilePrefab;
    [SerializeField] private GameObject bottleProjectilePrefab;
    [SerializeField] private GameObject mixedProjectilePrefab;

    [SerializeField] private int damage = 1;
    public int GetDamage()
    {
        return damage;
    }
    [SerializeField] private Transform playerTransform;

    private GameObject currentProjectile;
    private bool holdingProjectile = false;

    private void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }
         if (playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerController>().transform; // Find the player transform
        }
    }

    private void Update()
    {
        if (holdingProjectile && currentProjectile != null)
        {
            currentProjectile.transform.position = playerTransform.position;
            currentProjectile.transform.rotation = playerTransform.rotation;
        }

        // Fire the projectile when the left mouse button is clicked
        if (holdingProjectile && Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }
    }

    /// <summary>
    /// Create a projectile based on the items in the player's inventory
    /// </summary>
   public void CreateProjectile()
    {
        Debug.Log("Creating projectile...");

    string projectileType = playerInventory.GetProjectileType();

    if (string.IsNullOrEmpty(projectileType))
    {
        Debug.Log("No valid Projectile type found");
        return;
    }

    GameObject projectilePrefab = GetProjectilePrefab(projectileType);

    if (projectilePrefab != null)
    {
        Debug.Log("Creating projectile: " + projectileType);

        // Offset to place the projectile above the player's head
        Vector3 offset = new Vector3(0, 0.5f, 0); 
        Vector3 spawnPosition = playerTransform.position + offset;

        currentProjectile = Instantiate(projectilePrefab, spawnPosition, playerTransform.rotation);

        // Parent it to the player so it follows them
        currentProjectile.transform.parent = playerTransform;

        holdingProjectile = true;

        playerInventory.RemoveUsedItems();
    }
    else
    {
        Debug.Log("No valid Projectile prefab found");
    }
    }

    /// <summary>
    /// Fire the current projectile towards the mouse position
    /// </summary>
    private void FireProjectile()
    {
        if (currentProjectile == null) return;

        Debug.Log("Firing projectile...");

        currentProjectile.transform.parent = null;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - currentProjectile.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rb = currentProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * 35f; // projectile speed 
            AudioManager.Instance.PlayShootSound();
        }
        else
        {
            Debug.LogError("Projectile has no Rigidbody2D!");
        }

        holdingProjectile = false;
        currentProjectile = null;
    }

    /// <summary>
    /// Get the correct projectile prefab based on the type
    /// </summary>
    /// <param name="projectileType"></param>
    /// <returns></returns>
    private GameObject GetProjectilePrefab(string projectileType)
    {
        switch (projectileType)
        {
            case "WoodWoodWood":
                //Debug.Log("Created WoodWoodWood projectile");
                return woodProjectilePrefab;
            case "RockRockRock":
                //Debug.Log("Created RockRockRock projectile");
                return rockProjectilePrefab;
            case "BottleBottleBottle":
                //Debug.Log("Created BottleBottleBottle projectile");
                return bottleProjectilePrefab;
            default:
                //Debug.Log("Created Mixed projectile");
                return mixedProjectilePrefab;  
        }
    }
    
    /// <summary>
    /// Check for collision with an enemy and apply damage
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile collided with: " + other.gameObject.name);  

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Projectile hit an enemy!");
            HealthSystem enemyHealth = other.GetComponent<HealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Enemy took " + damage + " damage.");
            }
            Destroy(gameObject); // Destroy the projectile on impact
        }
    }
}
