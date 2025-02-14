using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 3;
    [SerializeField] private float itemMagnetDistance = 2f;

    private List<string> inventory = new List<string>();
    
    public void AddItem(GameObject item)
    {
        if (inventory.Count >= inventorySize) return;

        string itemName = item.name.Replace("(Clone)", "").Trim();
        inventory.Add(itemName);
        Debug.Log("Item added to inventory: " + itemName);
        Destroy(item);
    }

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
        Debug.Log("Inventory cleared!");
        inventory.Clear(); // Clear inventory when a projectile is created
    }   
}
