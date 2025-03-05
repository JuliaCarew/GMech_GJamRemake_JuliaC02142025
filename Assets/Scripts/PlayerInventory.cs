using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private int inventorySize = 3;
    private List<string> inventory = new List<string>();
    
    /// <summary>
    /// Add an item to the player's inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        if (inventory.Count >= inventorySize) return;

        string itemName = item.name.Replace("(Clone)", "").Trim();
        inventory.Add(itemName);
        //Debug.Log("Item added to inventory: " + itemName);
        Destroy(item);

        uiManager.UpdateInventoryUI(inventory); // Update UI

        //check items in inventory
        //create projectile
        if (inventory.Count == inventorySize)
        {
            //Debug.Log("Inventory full! Ready to create projectile.");
            FindObjectOfType<Projectiles>().CreateProjectile();
        }
    }

    /// <summary>
    /// Get the type of projectile based on the items in the inventory
    /// </summary>
    /// <returns></returns>
    public string GetProjectileType()
    {
        if (inventory.Count == 0) return null;

        // Sort items to ensure consistent combinations (e.g., "BottleWoodRock" is the same as "RockWoodBottle")
        List<string> sortedInventory = new List<string>(inventory);
        sortedInventory.Sort();

        // Combine items into a single string
        return string.Join("", sortedInventory);
    }

    public void RemoveUsedItems()
    {
        //Debug.Log("Inventory cleared!");
        inventory.Clear(); // Clear inventory when a projectile is created
    }   
}
