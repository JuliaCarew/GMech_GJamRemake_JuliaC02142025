using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // make this a singleton
    [SerializeField] private UIManager uiManager;
    GameManager gameManager;
    private Item nearbyItem;
    private GuessCrate guessCrate;
    private bool isNearGuessCrate = false;

    [SerializeField] private int inventorySize = 5;
    public List<string> inventory = new List<string>();
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //item = GetComponent<Item>(); // recognize object that player is colliding with
    }
    /// <summary>
    /// Add an item to the player's inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        if (inventory.Count >= inventorySize) return;

        string itemName = item.name.Replace("(Clone)", "").Trim();
        inventory.Add(itemName);
        Debug.Log("Item added to inventory: " + itemName);
        Destroy(item);

        uiManager.UpdateInventoryUI(inventory); // Update UI

        //check items in inventory
        //create projectile
        if (inventory.Count == inventorySize)
        {
            //Debug.Log("Inventory full! Ready to create projectile.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            nearbyItem = other.GetComponent<Item>();
        }
        if (other.CompareTag("GuessCrate"))
        {
            guessCrate = other.GetComponent<GuessCrate>();
            isNearGuessCrate = true;
            Debug.Log("Player entered Guess Crate zone.");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            nearbyItem = null;
        }
        else if (other.CompareTag("GuessCrate"))
        {
            isNearGuessCrate = false;
            guessCrate = null;
            Debug.Log("Player left Guess Crate zone.");
        }
    }
    
    private void Update(){
        if (nearbyItem != null) // item input
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                nearbyItem.PickUp();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                nearbyItem.Drop();
            }
        }
        if (isNearGuessCrate && Input.GetKeyDown(KeyCode.F)) // guess input
        {
            Debug.Log("Reading F key input. Submitting Guess.");
            guessCrate.SubmitGuess();
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

    // make method to get rid of any item from inventory by clicking and dragging it out of the inventory UI (use the UIManager script)
    public void RemoveItem(string itemName) // fix ??
    {
        // need to read left mouse button click and drag to recognize what item is selected and update position
        inventory.Remove(itemName);
        uiManager.UpdateInventoryUI(inventory); // Update UI
    }
}
// next wednesday animedia fest March 26th 8:30-9:30 lunch davis hall 12:00-1:00