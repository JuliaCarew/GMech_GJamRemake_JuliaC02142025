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
    [SerializeField] private GameObject hintUI;
    [SerializeField] private TextMeshProUGUI hintTextComponent;

    [Header("Guess Feedback UI")]
    [SerializeField] private GameObject correctGuessUI;
    [SerializeField] private TextMeshProUGUI correctGuessText;
    [SerializeField] private GameObject wrongGuessUI;

    [Header("HUD UI")]
    [SerializeField] private GameObject controlHUD;
    [SerializeField] private GameObject clearHUD;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        HideRiddleUI();
        HideHintUI();
        HideCorrectGuessUI();
        HideWrongGuessUI();
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
   
    public void UpdateRiddle(string riddleText)
    {
        if (riddleTextUI != null && riddleTextComponent != null)
        {
            riddleTextComponent.text = riddleText;
            riddleTextUI.SetActive(true);
        }
    }
    public void ShowHint(string hintText)
    {
        if (hintUI != null && hintTextComponent != null)
        {
            hintTextComponent.text = hintText;
            hintUI.SetActive(true);
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
            Debug.LogWarning("CorrectGuessUI or CorrectGuessText is not assigned.");
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
