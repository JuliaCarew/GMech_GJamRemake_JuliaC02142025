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
    public Item item; // Reference to the Item script to respawn letter set
    public GameObject mainMenu;

    [System.Serializable]
    public class ObstaclePuzzleSetup
    {
        public ObstaclePuzzle obstacleObject;
        public string puzzleWord;
    }

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
        public List<ObstaclePuzzleSetup> obstaclePuzzles = new List<ObstaclePuzzleSetup>();
        [HideInInspector]
        public List<GameObject> spawnedObstacles = new List<GameObject>();
        // tilemap that should be active for this scene
        public int[] activeTilemapIndices; // 0 = first, 1 = second, 2 = third
    }

    public List<LevelSetup> levels = new List<LevelSetup>();
    public int currentLevelIndex = 0;
    
    public void Start()
    {
        mainMenu.SetActive(true); // enable main menu UI
        
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }

        // Disable all obstacles at start
        foreach (LevelSetup level in levels)
        {
            foreach (ObstaclePuzzleSetup obstacle in level.obstaclePuzzles)
            {
                if (obstacle.obstacleObject != null)
                {
                    obstacle.obstacleObject.gameObject.SetActive(false);
                }
            }
        }

        LoadCurrentLevel();
    }

    
    public void LoadCurrentLevel()
{
    if (currentLevelIndex < levels.Count)
    {
        mainMenu.SetActive(false); // disable main menu UI
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

            // Disable all obstacles from this level
            foreach (ObstaclePuzzleSetup obstacle in level.obstaclePuzzles)
            {
                if (obstacle.obstacleObject != null)
                {
                    obstacle.obstacleObject.gameObject.SetActive(false);
                }
            }
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

        // Activate current level's obstacles and set up their puzzle words
        foreach (ObstaclePuzzleSetup obstacleSetup in currentLevel.obstaclePuzzles)
        {
            if (obstacleSetup.obstacleObject != null)
            {
                // Configure the obstacle puzzle
                obstacleSetup.obstacleObject.uiManager = uiManager;
                obstacleSetup.obstacleObject.levelManager = this;
                obstacleSetup.obstacleObject.SetSecretWord(obstacleSetup.puzzleWord);
                    
                // Activate the obstacle
                obstacleSetup.obstacleObject.gameObject.SetActive(true);
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
    SetActiveTilemapsForCurrentLevel();
}

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        AudioManager.Instance.PlayWinSound();
        
        if (currentLevelIndex < levels.Count)
        {
            Debug.Log("LevelManager: loading next level...");
            uiManager.HideHint();
            
            LoadCurrentLevel();
            // Set active tilemaps based on level configuration
            SetActiveTilemapsForCurrentLevel();
            
        }
        else
        {
            // All levels completed
            SceneManager.LoadScene("WinScene"); 
        }
    }

    private void SetActiveTilemapsForCurrentLevel()
    {
        LevelSetup currentLevel = levels[currentLevelIndex];
    
        // Create boolean array for tilemaps
        bool[] activeStates = new bool[gameManager.playerController.tilemapData.Length];
    
        // Set all to false initially
        for (int i = 0; i < activeStates.Length; i++)
        {
            activeStates[i] = false;
        }
    
        // Set specified indices to true
        if (currentLevel.activeTilemapIndices != null)
        {
            foreach (int index in currentLevel.activeTilemapIndices)
            {
                if (index >= 0 && index < activeStates.Length)
                {
                    activeStates[index] = true;
                }
            }
        }
    
        // Apply tilemap states
        gameManager.playerController.SetActiveTilemaps(activeStates);
    }
}
