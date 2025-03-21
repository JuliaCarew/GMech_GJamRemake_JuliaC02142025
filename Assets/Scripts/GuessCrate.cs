using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuessCrate : MonoBehaviour
{
    // manage and recognize unique riddles/words per level
    public UIManager uiManager;
    private PlayerInventory playerInventory;    
    public string secretWord { get; private set; }
    private string guessWord;
    public int guessCount;

    // ref. variable list of letters in inventory - compare if its secretword
    // ref. variable riddleUI in UIManager - update w unique text
    // ref. alphabet prefabs to initialize letter set (make an array of chars?/string?)
    private void Start(){
        playerInventory = FindObjectOfType<PlayerInventory>();
        secretWord = "CHAIR";
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

    public void SubmitGuess(){
        string playerWord = string.Join("", playerInventory.inventory);
        guessWord = playerWord.ToUpper();

        Debug.Log("Submitting guess...");
       
        if (guessWord == secretWord)
        {
            Debug.Log("You got it!");
            uiManager.ShowCorrectGuessUI("CHAIR");
            // only show for a few seconds
            // go to next level
        }
        else
        {
            Debug.Log("Wrong answer. Try again.");
            uiManager.ShowWrongGuessUI();
            guessCount++;
        }
    }
}
