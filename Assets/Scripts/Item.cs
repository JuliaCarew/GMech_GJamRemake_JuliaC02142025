using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Transform player;
    private PlayerInventory playerInventory;
    private bool isMagnetized = false;

    [SerializeField] private float magnetSpeed = 5f;
    [SerializeField] private float magnetRange = 2f;

    private void Start()
    {
        Debug.Log("Item spawned!");
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            // If the Player has a child object for the sprite, reference that
            Transform spriteTransform = playerObject.transform.Find("PlayerSprite"); 

            player = spriteTransform != null ? spriteTransform : playerObject.transform;
            playerInventory = playerObject.GetComponent<PlayerInventory>();
        }
    }

    private void Update()
    {
        if (player == null || playerInventory == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < magnetRange && !isMagnetized)
        {
            StartCoroutine(ItemMagnetize());
        }
    }

    private IEnumerator ItemMagnetize()
    {
        isMagnetized = true;

        while (Vector2.Distance(transform.position, player.position) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, Time.deltaTime * magnetSpeed);
            yield return null;
        }

        if (playerInventory != null)
        {
            playerInventory.AddItem(gameObject); // Send the item to inventory
        }
    }
}
