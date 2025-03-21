using UnityEngine;

public class Item : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public GameObject playerObject;

    private void Awake()
    {
        if (playerObject != null)
        {
            if (playerObject != null && playerInventory == null)
            {
                playerInventory = playerObject.GetComponent<PlayerInventory>();
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
