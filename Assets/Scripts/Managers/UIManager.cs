using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour // singleton
{
    [Header("Inventory UI")]
    [SerializeField] private List<Image> itemSlots; // UI Image slots
    [SerializeField] private Sprite emptySprite,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z; 

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;

    // implement hint/riddle UI panels that can be easily called to show at diffeent times for each level
    [Header("Riddles & Hints UI")]
    [SerializeField] private GameObject riddleTextUI;
    [SerializeField] private TextMeshProUGUI riddleTextComponent;
    [SerializeField] public GameObject hintUI;
    [SerializeField] public TextMeshProUGUI hintTextComponent;

    [Header("Guess Feedback UI")]
    [SerializeField] private GameObject correctGuessUI;
    [SerializeField] private TextMeshProUGUI correctGuessText;
    [SerializeField] private GameObject wrongGuessUI;

    [Header("HUD UI")]
    [SerializeField] private GameObject guessCrateUI;
    [SerializeField] private GameObject clearHUD;
    [SerializeField] private GameObject obsPuzzleHUD;
    [SerializeField] private GameObject tutorialControlHUD; // CONTROLS

    [Header("Dictionary UI")]
    [SerializeField] private GameObject dictionaryUI;
    [SerializeField] private TextMeshProUGUI dictionaryTextComponent; // display player's correct secret words
    [SerializeField] private GameObject dictionaryUpdated;
    [SerializeField] private TextMeshProUGUI dictionaryUpdatedText;
    [SerializeField] private TextMeshProUGUI dictionaryGuesses; // display player's guessed words
    private List<string> dictionary = new List<string>();
    private List<string> dictionaryGuessedWords = new List<string>();



    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        HideRiddleUI();
        HideHintUI();
        HideCorrectGuessUI();
        HideWrongGuessUI();
        HideDictionary();
    }

    public void UpdateInventoryUI(List<string> inventory)
    {
        Debug.Log("UIManager: Updating invenotry UI.");
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < inventory.Count)
            {
                itemSlots[i].sprite = GetItemSprite(inventory[i]);
                itemSlots[i].enabled = true;
            }
            else
            {
                itemSlots[i].sprite = emptySprite;
                itemSlots[i].enabled = false;
            }
        }
    }
    public void ClearInventoryUI()
    {
        // Reset all inventory slots to empty sprite and disable
        foreach (Image slot in itemSlots)
        {
            slot.sprite = emptySprite;
            slot.enabled = false;
        }
        Debug.Log("Inventory UI cleared");
    }

    public void UpdateDictionary(string unlockedWords){
        // each time the player guesses a secret word or puzzleword right, add it to the dictionary
        if (dictionaryUI != null && dictionaryTextComponent != null)
        {
            string words = string.Join(", ", unlockedWords);
            dictionaryGuessedWords.Add(unlockedWords); // add to the dictionary list
            dictionaryTextComponent.text = string.Join(", ", dictionaryGuessedWords); // update the text component with the new list
            AudioManager.Instance.PlayAddToDictionarySound();
            Debug.Log($"Dictionary updated: " + unlockedWords + " added to dictionary: " + string.Join(", ", dictionaryGuessedWords));

        }
        else
        {
            Debug.LogWarning("UIManager: Dictionary UI or Text Component is not assigned.");
        }
    }
    public void UpdateDictionaryGuesses(string guessedWords)
    {
        // each time the player guesses a secret word or puzzleword right, add it to the dictionary
        if (dictionaryUI != null && dictionaryGuesses != null)
        {
            string words = string.Join(", ", guessedWords);
            dictionary.Add(guessedWords); // add to the dictionary list
            dictionaryGuesses.text = string.Join(", ", dictionary); // update the text component with the new list
            AudioManager.Instance.PlayAddToDictionarySound();
            Debug.Log($"Dictionary GUESS updated: " + guessedWords + " added to dictionary: " + string.Join(", ", dictionary));

        }
        else
        {
            Debug.LogWarning("UIManager: Dictionary UI or Text Component is not assigned.");
        }
    }
    public void ShowDictionary()
    {
        if (dictionaryUI != null)
        {
            dictionaryUI.SetActive(true);
            AudioManager.Instance.PlayOpenDictionarySound();
            Debug.Log("Dictionary UI shown.");
        }
    }
    public void HideDictionary()
    {
        if (dictionaryUI != null)
        {
            dictionaryUI.SetActive(false);
            //dictionaryTextComponent.text = ""; // Clear the text when hiding the UI
            Debug.Log("Dictionary UI hidden.");
        }
    }

    private Sprite GetItemSprite(string itemName)
    {
        return itemName.ToUpper() switch // change to alphabet
        {
            "A" => a,
            "B" => b,
            "C" => c,
            "D" => d,
            "E" => e,
            "F" => f,
            "G" => g,
            "H" => h,
            "I" => i,
            "J" => j,
            "K" => k,
            "L" => l,
            "M" => m,
            "N" => n,
            "O" => o,
            "P" => p,
            "Q" => q,
            "R" => r,
            "S" => s,
            "T" => t,
            "U" => u,
            "V" => v,
            "W" => w,
            "X" => x,
            "Y" => y,
            "Z" => z,
            _ => emptySprite,
        };
    }
   
    public void UpdateRiddle(string riddleText)
    {
        if (riddleTextUI != null && riddleTextComponent != null)
        {
            Debug.Log("UIManager: Updating riddle text.");
            riddleTextComponent.text = riddleText;
            riddleTextUI.SetActive(true);           
        }
    }
    public void ShowHint(string hintText)
    {
        if (hintUI != null && hintTextComponent != null)
        {
            Debug.Log("UIManager: Updating hint text.");
            hintTextComponent.text = hintText;
            hintUI.SetActive(true);
            AudioManager.Instance.PlayHintSound();
        }
    }
    public void HideHint()
    {
        if (hintUI != null)
        {
            hintUI.SetActive(false);
        }
    }
    public void HideRiddleUI()
    {
        if (riddleTextUI != null)
        {
            riddleTextUI.SetActive(false);
        }
    }

    public void HideHintUI()
    {
        if (hintUI != null)
        {
            hintUI.SetActive(false);
        }
    }

    private void ShowGameOverScreen()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Activate the Game Over panel when the player dies
        }
    }
    public void ShowGuessHUD()
    {
        guessCrateUI.SetActive(true);
    }
    public void HideGuessHUD()
    {
        guessCrateUI.SetActive(false);
    }
    public void ShowClearHUD()
    {
        clearHUD.SetActive(true);
    }
    public void HideClearHUD()
    {
        clearHUD.SetActive(false);
    }

    public void ShowCorrectGuessUI(string word)
    {
        StartCoroutine(CorrectGuessCoroutine(word));
    }

    private IEnumerator CorrectGuessCoroutine(string word)
    {
        if (correctGuessUI != null && correctGuessText != null)
        {
            correctGuessText.text = word;
            correctGuessUI.SetActive(true);
            yield return new WaitForSeconds(3);
            correctGuessUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UIManager: CorrectGuessUI or CorrectGuessText is not assigned.");
        }
    }
    public void ShowWrongGuessUI()
    {
        StartCoroutine(WrongGuessCoroutine());
    }

    private IEnumerator WrongGuessCoroutine()
    {
        wrongGuessUI.SetActive(true);
        Debug.Log("Wrong guess UI shown.");
        yield return new WaitForSeconds(2);
        wrongGuessUI.SetActive(false);
        Debug.Log("Wrong guess UI hidden.");
    }

    public IEnumerator DictionaryUpdated()
    {
        dictionaryUpdated.SetActive(true);
        dictionaryUpdatedText.text = "Dictionary Updated!";
        yield return new WaitForSeconds(2);
        dictionaryUpdated.SetActive(false);
    }

    public void HideCorrectGuessUI()
    {
        if (correctGuessUI != null)
        {
            correctGuessUI.SetActive(false);
        }
    }

    public void HideWrongGuessUI()
    {
        if (wrongGuessUI != null)
        {
            wrongGuessUI.SetActive(false);
        }
    }

    public void ShowObstacleHUD()
    {
        obsPuzzleHUD.SetActive(true);
    }
    public void HideObstacleHUD()
    {
        obsPuzzleHUD.SetActive(false); // getting error object already destroyed but still trying to access it
    }

    public void ShowControlUI()
    {
        tutorialControlHUD.SetActive(true);
    }
    public void HideControlUI()
    {
        tutorialControlHUD.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
