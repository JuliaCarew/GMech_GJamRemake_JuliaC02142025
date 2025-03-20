using UnityEngine;

public class Item : MonoBehaviour
{
    GameManager gameManager;
    public PlayerInventory playerInventory;
    public GameObject playerObject;

    private void Awake()
    {
        //GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerInventory = playerObject.GetComponent<PlayerInventory>(); // getting null reference 
            if (playerInventory == null)
            {
                Debug.LogError("PlayerInventory is null. Ensure the player has a PlayerInventory component.");
            }
        }
    }

    public void PickUp() {
        if (playerInventory == null)
        {
            Debug.LogError("Cannot pick up item. PlayerInventory is still null.");
            return;
        }
        Debug.Log("Player picked up item!");
        AudioManager.Instance.PlayPickupSound();
        playerInventory.AddItem(gameObject);
        Destroy(gameObject);
    }
    public void Drop() {
        if (playerInventory == null)
        {
            Debug.LogError("Cannot drop item. PlayerInventory is still null.");
            return;
        }
        
        Debug.Log("Player dropped item!");
        AudioManager.Instance.PlayDropSound();
        playerInventory.RemoveItem(gameObject.name);
    }
}
