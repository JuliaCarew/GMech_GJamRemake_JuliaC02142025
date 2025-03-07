using UnityEngine;

public class Item : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            gameManager.playerInventory = playerObject.GetComponent<PlayerInventory>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"Collided with: {other.gameObject.name}");
        if (other.CompareTag("Player") ) //&& playerInventory != null
        {
            //Debug.Log("Player picked up item!");
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            AudioManager.Instance.PlayPickupSound();
            
            if (playerInventory != null)
            {
                playerInventory.AddItem(gameObject);
            }
        }
    }
}
