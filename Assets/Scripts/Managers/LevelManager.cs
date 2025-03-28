using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public UIManager uiManager;
    public GuessCrate guessCrate;
    public PlayerInventory playerInventory;

    [System.Serializable]
    public class LevelSetup
    {
        public GameObject map;
        public GameObject letterSet;
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
                level.map.SetActive(false);
                level.letterSet.SetActive(false);
            }

            // Activate current level's map and letter set
            currentLevel.map.SetActive(true);
            currentLevel.letterSet.SetActive(true);

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
            LoadCurrentLevel();
        }
        else
        {
            // All levels completed
            SceneManager.LoadScene("WinScene"); 
        }
    }
}
// handle scene transitions to load the next level when the player reaches the end of the current level
// when the player opens the door load next scene (have public list to drop scene levels in inspector?)

// Level set up
// can either do so through inspector or reading text files through code
// may be easier since working with letter [refabs to do inspector