using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    private static PlayerInventory cachedPlayerInventory;

    private void Start()
    {
        AssignPlayerInventory();
    }

    private void AssignPlayerInventory()
    {
        // If we haven't cached the PlayerInventory yet, find it
        if (cachedPlayerInventory == null)
        {
            // Find player object if not already assigned
            if (playerObject == null)
            {
                playerObject = GameObject.FindGameObjectWithTag("Player");
            }

            // Try to get PlayerInventory component
            if (playerObject != null)
            {
                cachedPlayerInventory = playerObject.GetComponent<PlayerInventory>();
            }
        }

        // Log error if still can't find PlayerInventory
        if (cachedPlayerInventory == null)
        {
            Debug.LogError($"Item {gameObject.name}: Cannot find PlayerInventory. Ensure Player object has PlayerInventory component!");
        }
    }

    public void PickUp()
    {
        // Reassign if lost reference
        if (cachedPlayerInventory == null)
        {
            AssignPlayerInventory();
        }

        if (cachedPlayerInventory != null)
        {
            Debug.Log($"Item {gameObject.name}: Picked up!");
            AudioManager.Instance.PlayPickupSound();
            cachedPlayerInventory.AddItem(gameObject);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError($"Item {gameObject.name}: Cannot pick up. No PlayerInventory found!");
        }
    }

    public void Drop()
    {
        // Reassign if lost reference
        if (cachedPlayerInventory == null)
        {
            AssignPlayerInventory();
        }

        if (cachedPlayerInventory != null)
        {
            Debug.Log($"Item {gameObject.name}: Dropped!");
            AudioManager.Instance.PlayDropSound();
            cachedPlayerInventory.RemoveItem(gameObject.name);
        }
        else
        {
            Debug.LogError($"Item {gameObject.name}: Cannot drop. No PlayerInventory found!");
        }
    }
}