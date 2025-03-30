using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuessCrate : MonoBehaviour
{
    // manage and recognize unique riddles/words per level
    public UIManager uiManager;
    private PlayerInventory playerInventory;
    public LevelManager levelManager;

    public string secretWord;
    private string guessWord;
    public int guessCount;

    // ref. variable list of letters in inventory - compare if its secretword
    // ref. variable riddleUI in UIManager - update w unique text
    // ref. alphabet prefabs to initialize letter set (make an array of chars?/string?)
    private void Start(){
        playerInventory = FindObjectOfType<PlayerInventory>();

        if (levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ShowControlHUD();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.HideControlHUD();
        }
    }
    public void SetSecretWord(string word)
    {
        secretWord = word.ToUpper();
        Debug.Log("GuessCrate: Secret word set: " + secretWord);
    }

    public void SubmitGuess(){
        string playerWord = string.Join("", playerInventory.inventory);
        guessWord = playerWord.ToUpper();

        Debug.Log("Submitting guess...");
       
        if (guessWord == secretWord)
        {
            Debug.Log("You got it!");
            uiManager.ShowCorrectGuessUI(secretWord);
            // only show for a few seconds
            // go to next level
            StartCoroutine(LoadNextLevelAfterDelay());
        }
        else {
            Debug.Log("Wrong answer. Try again.");
            uiManager.ShowWrongGuessUI();
            guessCount++;
        }
    }
    
    private IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Wait 2 seconds to show the correct guess UI
        
        // Clear inventory for next level
        playerInventory.RemoveUsedItems();
        
        // Load next level
        levelManager.LoadNextLevel();
    }
}
