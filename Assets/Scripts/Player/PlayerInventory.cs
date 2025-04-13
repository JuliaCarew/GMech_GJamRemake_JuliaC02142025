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
    private ClearCrate clearCrate;
    public ObstaclePuzzle obstaclePuzzle; // make invis in inspector?
    private bool isNearGuessCrate = false;
    private bool isNearClearCrate = false;
    private bool isNearObstaclePuzzle = false;


    [SerializeField] private int inventorySize = 5;
    public List<string> inventory = new List<string>();

    private bool controlHUDisOn = true;
    private bool dictionaryisOn = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        nearbyItem = GetComponent<Item>(); // recognize object that player is colliding with
    }
    /// <summary>
    /// Add an item to the player's inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item)
    {
        if (inventory.Count >= inventorySize) return;

        string itemName = item.name;
        inventory.Add(itemName);
        Debug.Log("Item added to inventory: " + itemName);
        Destroy(item);

        uiManager.UpdateInventoryUI(inventory); // Update UI

        //check items in inventory
        //create projectile
        if (inventory.Count == inventorySize)
        {
            Debug.Log("Inventory full!");
            return; // make it so the player can't pick up any more
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
        if (other.CompareTag("ClearCrate"))
        {
            clearCrate = other.GetComponent<ClearCrate>();
            isNearClearCrate = true;
            Debug.Log("Player entered CLEAR Crate zone.");
        }
        if (other.CompareTag("ObstaclePuzzle"))
        {
            obstaclePuzzle = other.GetComponent<ObstaclePuzzle>();
            isNearObstaclePuzzle = true;
            Debug.Log("Player entered Obstacle Puzzle zone.");
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
        else if (other.CompareTag("ClearCrate"))
        {
            isNearClearCrate = false;
            clearCrate = null;
            Debug.Log("Player left Clear Crate zone.");
        }
        else if (other.CompareTag("ObstaclePuzzle"))
        {
            isNearObstaclePuzzle = false;
            obstaclePuzzle = null;
            Debug.Log("Player left Obstacle Puzzle zone.");
        }
    }
    
    private void Update(){
        if (Input.GetKeyDown(KeyCode.T)){ // toggle controls with T
            if (controlHUDisOn)
            {
                uiManager.HideControlUI();
                controlHUDisOn = false;
            }
            else if (!controlHUDisOn)
            {
                uiManager.ShowControlUI();
                controlHUDisOn = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) // press Q to toggle dictionary
        {
            if (dictionaryisOn)
            {
                uiManager.HideDictionary();
                dictionaryisOn = false;
            }
            else if (!dictionaryisOn)
            {
                uiManager.ShowDictionary();
                dictionaryisOn = true;
            }
        }
        if (nearbyItem != null) // item input
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                nearbyItem.PickUp();
            }
        }
        if (isNearGuessCrate && Input.GetKeyDown(KeyCode.F)) // guess input
        {
            Debug.Log("Reading F key input. Submitting Guess.");
            guessCrate.SubmitGuess();
        }
        if (isNearClearCrate && Input.GetKeyDown(KeyCode.F)) {
            Debug.Log("Reading F key input. Clearing player inventory");
            clearCrate.Clear();           
        }
        if (isNearObstaclePuzzle && Input.GetKeyDown(KeyCode.F)) // obstacle input
        {
            Debug.Log("Reading F key input. Submitting Guess.");
            // submit guess to obstacle puzzle
            obstaclePuzzle.SubmitGuess();
        }
    }    

    public void RemoveUsedItems()
    {
        //Debug.Log("Inventory cleared!");
        inventory.Clear(); // Clear inventory when a projectile is created
        uiManager.UpdateInventoryUI(inventory);
    }   

    public void ClearInventory()
    {
        // Clear all items from the inventory
        inventory.Clear();
        AudioManager.Instance.PlayClearInventorySound();
        Debug.Log("Inventory cleared");
    }
}
