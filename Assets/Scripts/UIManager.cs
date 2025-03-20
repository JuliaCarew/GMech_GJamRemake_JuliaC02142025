using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private List<Image> itemSlots; // UI Image slots
    [SerializeField] private Sprite emptySprite,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z; 

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;

    private HealthSystem playerHealthSystem;

    // implement hint/riddle UI panels that can be easily called to show at diffeent times for each level
    [Header("Riddles")]
    [SerializeField] private GameObject riddleTextUI;
    [SerializeField] private GameObject HintUI;

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
   
    private void UpdateRiddle()
    {
        riddleTextUI.SetActive(true);
    }
    private void ShowHint()
    {
        HintUI.SetActive(true);
    }

    private void ShowGameOverScreen()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Activate the Game Over panel when the player dies
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
