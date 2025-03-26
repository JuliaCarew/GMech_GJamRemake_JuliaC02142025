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
    [Header("Riddles")]
    [SerializeField] private GameObject riddleTextUI;
    [SerializeField] private GameObject HintUI;
    [SerializeField] private GameObject correctGuessUI;
    [SerializeField] private TextMeshProUGUI correctGuessText;
    [SerializeField] private GameObject wrongGuessUI;

    [Header("HUD UI")]
    [SerializeField] private GameObject controlHUD;
    [SerializeField] private GameObject clearHUD;

    [Header("Unique Riddles")]
    [SerializeField] private GameObject riddleTextUICHAIR;
    [SerializeField] private GameObject riddleTextUISHIRT;
    [SerializeField] private GameObject riddleTextUILIGHT;

    [Header("Unique Hints")]
    [SerializeField] private GameObject hintUICHAIR;
    [SerializeField] private GameObject hintUISHIRT;
    [SerializeField] private GameObject hintUILIGHT;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpdateInventoryUI(List<string> inventory)
    {
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
   
    public void UpdateRiddle(string secretWord)
    {
        riddleTextUI.SetActive(true);
        // set up multiple riddle GameObjects to call through method by secretword
        if (secretWord == "chair")
        {
            riddleTextUICHAIR.SetActive(true);
        }
        if (secretWord == "shirt")
        {
            riddleTextUISHIRT.SetActive(true);
        }
        if (secretWord == "light")
        {
            riddleTextUILIGHT.SetActive(true);
        }
    }
    public void ShowHint(string secretWord)
    {
        HintUI.SetActive(true);
        // set up multiple hints to call through method
        if (secretWord == "chair")
        {
            hintUICHAIR.SetActive(true);
        }
        if (secretWord == "shirt")
        {
            hintUISHIRT.SetActive(true);
        }
        if (secretWord == "light")
        {
            hintUILIGHT.SetActive(true);
        }
    }

    private void ShowGameOverScreen()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Activate the Game Over panel when the player dies
        }
    }
    public void ShowControlHUD()
    {
        controlHUD.SetActive(true);
    }
    public void HideControlHUD()
    {
        controlHUD.SetActive(false);
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
        if (correctGuessUI != null && correctGuessText != null)
        {
            correctGuessText.text = word; // Display the provided word
            correctGuessUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("CorrectGuessUI or CorrectGuessText is not assigned.");
        }
    }
    public void ShowWrongGuessUI()
    {
        StartCoroutine(WrongGuessUI());
    }
    public IEnumerator WrongGuessUI()
    {
        wrongGuessUI.SetActive(true);
        Debug.Log("Wrong guess UI shown.");
        yield return new WaitForSeconds(2);
        wrongGuessUI.SetActive(false);
        Debug.Log("Wrong guess UI hidden.");
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
