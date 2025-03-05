using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private List<Image> itemSlots; // UI Image slots
    [SerializeField] private Sprite woodSprite, rockSprite, bottleSprite, emptySprite; // Item sprites

    [Header("Health UI")]
    [SerializeField] private List<Image> healthIcons; // UI Heart slots
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;

    private HealthSystem playerHealthSystem; 

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            playerHealthSystem = player.GetComponent<HealthSystem>();

            if (playerHealthSystem != null)
            {
                playerHealthSystem.OnHealthChanged += UpdateHealthUI; // Subscribe to health changes
                playerHealthSystem.OnDeath  += ShowGameOverScreen;
                UpdateHealthUI(playerHealthSystem.CurrentHealth); // Initialize UI with correct health
            }
        }
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
        return itemName switch
        {
            "Wood" => woodSprite,
            "Rock" => rockSprite,
            "Bottle" => bottleSprite,
            _ => emptySprite,
        };
    }

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            if (i < currentHealth)
            {
                healthIcons[i].sprite = fullHeartSprite; // Full heart for current health
            }
            else
            {
                healthIcons[i].sprite = emptyHeartSprite; // Empty heart for missing health
            }
        }
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
        Application.Quit(); // Quit the game
    }
}
