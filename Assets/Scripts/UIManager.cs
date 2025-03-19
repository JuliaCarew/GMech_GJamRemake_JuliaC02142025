using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private List<Image> itemSlots; // UI Image slots
    [SerializeField] private Sprite woodSprite, rockSprite, bottleSprite, emptySprite; // Item sprites (change to alphabet)

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
            }
            else
            {
                itemSlots[i].sprite = emptySprite;
            }
        }
    }

    private Sprite GetItemSprite(string itemName)
    {
        return itemName switch // change to alphabet
        {
            "Wood" => woodSprite,
            "Rock" => rockSprite,
            "Bottle" => bottleSprite,
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
