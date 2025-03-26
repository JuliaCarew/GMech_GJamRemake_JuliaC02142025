using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCrate : MonoBehaviour
{
    public UIManager uiManager;
    private PlayerInventory playerInventory;
    public LevelManager levelManager;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ShowClearHUD();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.HideClearHUD();
        }
    }

    public void Clear()
    {
        // clear inventory and update UI
        playerInventory.RemoveUsedItems();
        // respawn item set
        levelManager.Start(); // null ref. not set to instance of obj
    }
}
