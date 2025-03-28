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
        // Destroy existing letter set before respawning
        var currentLevel = levelManager.levels[levelManager.currentLevelIndex];
        if (currentLevel.letterSet != null)
        {
            Destroy(currentLevel.letterSet);
        }

        // Respawn the letter set
        currentLevel.letterSet = Instantiate(currentLevel.letterSet);
        
        // Reload the current level
        levelManager.LoadCurrentLevel(); 
    }
}
