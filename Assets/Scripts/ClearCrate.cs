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
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }
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
        // Get the current level
        var currentLevel = levelManager.levels[levelManager.currentLevelIndex];

        // Completely clear the player's inventory
        if (playerInventory != null)
        {
            playerInventory.ClearInventory(); 
        }

        // Clear the UI inventory
        if (uiManager != null)
        {
            uiManager.ClearInventoryUI(); 
        }

        // Destroy existing letter set
        if (currentLevel.letterSet != null)
        {
            Destroy(currentLevel.letterSet);
        }

        // Reload the current level, which will respawn the letter set
        levelManager.LoadCurrentLevel();
    }
}
