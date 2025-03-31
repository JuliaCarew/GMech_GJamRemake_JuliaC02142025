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
            uiManager.ShowGuessHUD();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.HideGuessHUD();
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
        uiManager.UpdateDictionaryGuesses(guessWord); // Update the dictionary with the player's guessed word

        if (guessWord == secretWord)
        {
            Debug.Log("You got it!");
            uiManager.ShowCorrectGuessUI(secretWord);
            uiManager.UpdateDictionary(secretWord); // Update the dictionary UI with the correct word
            uiManager.StartCoroutine(uiManager.DictionaryUpdated()); // Update the dictionary UI with the correct word
            
            AudioManager.Instance.PlayCorrectGuessSound();
            // go to next level
            StartCoroutine(LoadNextLevelAfterDelay());
        }
        else {
            Debug.Log("Wrong answer. Try again.");
            uiManager.ShowWrongGuessUI();
            // Play the wrong guess sound
            AudioManager.Instance.PlayWrongGuessSound();
            guessCount++;
        }
        if (guessCount == 3)
        {
            Debug.Log("you have guessed 3 times, giving hint...");
            string hint = levelManager.levels[levelManager.currentLevelIndex].hint;
            uiManager.StartCoroutine(uiManager.ShowHint(hint)); // Show the hint UI for 3 seconds
            return;
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
