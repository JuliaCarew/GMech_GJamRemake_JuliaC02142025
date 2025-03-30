using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public AudioManager audioManager;
    public CameraController cameraController;
    public Crosshair crosshair;
    public Item item;
    public LevelManager levelManager; 
    public PlayerController playerController;
    public PlayerInventory playerInventory;
    public UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Ensure GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager instances
        }
    }
}
