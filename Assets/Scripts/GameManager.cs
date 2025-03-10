using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioManager audioManager;
    public CameraController cameraController;
    public Crosshair crosshair;
    public Enemy enemy;
    public EnemyController enemyController;
    public HealthSystem healthSystem;
    public Item item;
    public ItemSpawner itemSpawner;
    public LevelManager levelManager; 
    public PlayerController playerController;
    public PlayerInventory playerInventory;
    public Projectiles projectiles;
    public UIManager uiManager;
}
