using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uiManager;
    public GuessCrate guessCrate;
    public PlayerInventory playerInventory;
    //public PlayerController playerController;
    public Item item; // Reference to the Item script to respawn letter set

    [System.Serializable]
    public class LevelSetup
    {
        public GameObject map;
        public GameObject letterSet;
        public GameObject letterSetPrefab; 
        public GameObject guessCratePrefab; 
        public GameObject clearCratePrefab; 
        public string secretWord;
        [TextArea]
        public string riddle;
        [TextArea]
        public string hint;
    }

    public List<LevelSetup> levels = new List<LevelSetup>();
    public int currentLevelIndex = 0;
    
    public void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }

        LoadCurrentLevel();
    }

    
    public void LoadCurrentLevel()
{
    if (currentLevelIndex < levels.Count)
    {
        LevelSetup currentLevel = levels[currentLevelIndex];

        // Deactivate all maps and letter sets first
        foreach (LevelSetup level in levels)
        {
            if (level.map != null)
                level.map.SetActive(false);
            if (level.letterSet != null)
                level.letterSet.SetActive(false);
            if (level.guessCratePrefab != null)
                level.guessCratePrefab.SetActive(false);
            if (level.clearCratePrefab != null)
                level.clearCratePrefab.SetActive(false);
        }

        // Respawn letter set if it was destroyed
        if (currentLevel.letterSet == null)
        {
            // Check if a prefab is assigned
            if (currentLevel.letterSetPrefab != null)
            {
                // Instantiate the prefab at its original position
                currentLevel.letterSet = Instantiate(
                currentLevel.letterSetPrefab, 
                currentLevel.letterSetPrefab.transform.position, 
                currentLevel.letterSetPrefab.transform.rotation
                );
    
                // Reassign inventory for all items in the letter set
                Item[] items = currentLevel.letterSet.GetComponentsInChildren<Item>();

                Debug.Log($"Respawned letter set for Level {currentLevelIndex + 1}");
            }
            else
            {
                Debug.LogError($"No letter set prefab assigned for Level {currentLevelIndex + 1}");
            }
        }

        // Activate current level's map and letter set
        if (currentLevel.map != null)
            currentLevel.map.SetActive(true);
        if (currentLevel.letterSet != null)
            currentLevel.letterSet.SetActive(true);
        if (currentLevel.guessCratePrefab != null)
            currentLevel.guessCratePrefab.SetActive(true);
        if (currentLevel.clearCratePrefab != null)
            currentLevel.clearCratePrefab.SetActive(true);

        // Update UI
        uiManager.UpdateRiddle(currentLevel.riddle);
        
        // Set secret word for GuessCrate
        guessCrate.SetSecretWord(currentLevel.secretWord);

        Debug.Log($"Loaded Level {currentLevelIndex + 1}: {currentLevel.secretWord}");
    }
    else
    {
        Debug.Log("All levels completed!");
        // game completion logic here
    }
}

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        
        if (currentLevelIndex < levels.Count)
        {
            Debug.Log("LevelManager: loading next level...");
            LoadCurrentLevel();
            if(currentLevelIndex == 1)
            {
                // Set the active tilemaps for the second level
                gameManager.playerController.SetActiveTilemaps(new bool[] { false, true, false });
            }
            else if(currentLevelIndex == 2)
            {
                // Set the active tilemaps for the third level
                gameManager.playerController.SetActiveTilemaps(new bool[] { false, false, true });
            }
            else if(currentLevelIndex == 3)
            {
                // Set the active tilemaps for the fourth level
                gameManager.playerController.SetActiveTilemaps(new bool[] { false, true, false });
            }
            else
            {
                // All levels completed
                SceneManager.LoadScene("WinScene"); 
            }
        }

    }
}
// handle scene transitions to load the next level when the player reaches the end of the current level
// when the player opens the door load next scene (have public list to drop scene levels in inspector?)

// Level set up
// can either do so through inspector or reading text files through code
// may be easier since working with letter [refabs to do inspector

// get level index reference when setting iswalkable bool in playercontroller script