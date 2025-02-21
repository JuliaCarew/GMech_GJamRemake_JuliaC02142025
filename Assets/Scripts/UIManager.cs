using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            healthIcons[i].sprite = i < currentHealth ? fullHeartSprite : emptyHeartSprite;
        }
    }

}
