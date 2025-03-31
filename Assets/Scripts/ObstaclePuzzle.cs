using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePuzzle : MonoBehaviour
{
    // ENUM
    [Header("Obstacle Types")]
    public ObstacleType obstacleType;
    public enum ObstacleType // sets the type of interaction for the object, swap between them in the inspector drop-down
    {
        Default,
        Deactivate, // swaps sprites and sets another object inactive
        Activate, // activates another object (like a door or lever)
        PlayAudio // plays an audio clip
    }

    public UIManager uiManager;
    private PlayerInventory playerInventory;
    public LevelManager levelManager;

    private string guessWord;
    public string puzzleWord;

    [SerializeField] private float deactivationDelay = 1.0f;
    [SerializeField] private GameObject visualObject; // The visual part to deactivate/animate
    [SerializeField] private Collider2D obstacleCollider; // The collider to disable

    [Header("Deactivate Settings")]
    [SerializeField] private GameObject objectToDeactivate;
    [SerializeField] private Sprite deactivatedSprite;
    
    [Header("Activate Settings")]
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private Sprite activatedSprite;
    
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip puzzleSolvedClip;

    private bool isPuzzleSolved = false;
    private SpriteRenderer spriteRenderer;

    public void Interact(){
        switch (obstacleType)
        {
            case ObstacleType.Default:
                StartCoroutine(ObstacleSolved());
                break;
            case ObstacleType.Deactivate:
                Deactivate();
                break;
            case ObstacleType.Activate:
                Activate();
                break;
            case ObstacleType.PlayAudio:
                PlayAudio();
                break;
        }
    }

    private void Awake()
    {
        // If visualObject isn't assigned, use this gameObject
        if (visualObject == null)
        {
            visualObject = this.gameObject;
        }
        
        // If collider isn't assigned, try to get it from this gameObject
        if (obstacleCollider == null)
        {
            obstacleCollider = GetComponent<Collider2D>();
        }

        // If audioSource isn't assigned, try to get it from this gameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            // If still null, add an AudioSource component
            if (audioSource == null && obstacleType == ObstacleType.PlayAudio)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }
    private void Start()
    {
        // Find references if not assigned
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory == null)
            {
                Debug.LogError("PlayerInventory not found in scene!");
            }
        }

        if (levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
            if (levelManager == null)
            {
                Debug.LogError("LevelManager not found in scene!");
            }
        }
        
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
            if (uiManager == null)
            {
                Debug.LogError("UIManager not found in scene!");
            }
        }
        
        // Make sure the tag is set correctly
        if (this.gameObject.tag != "ObstaclePuzzle")
        {
            Debug.LogWarning("ObstaclePuzzle object should have the 'ObstaclePuzzle' tag!");
            this.gameObject.tag = "ObstaclePuzzle";
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPuzzleSolved)
        {
            // Set this obstacle as the active one in the player's inventory script
            PlayerInventory playerInv = other.GetComponent<PlayerInventory>();
            if (playerInv != null)
            {
                playerInventory.obstaclePuzzle = this;
            }
            
            uiManager.ShowObstacleHUD();
            Debug.Log("Player entered ObstaclePuzzle zone: " + puzzleWord);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Clear the obstacle reference when player leaves
            PlayerInventory playerInv = other.GetComponent<PlayerInventory>();
            if (playerInv != null && playerInv.obstaclePuzzle == this)
            {
                playerInv.obstaclePuzzle = null;
            }
            
            uiManager.HideObstacleHUD();
        }
    }
    public void SetSecretWord(string word)
    {
        puzzleWord = word.ToUpper();
        Debug.Log("ObstaclePuzzle: Secret word set: " + puzzleWord);
    }

    public void SubmitGuess(){
        if (isPuzzleSolved)
            return;

        // Get the player inventory if it's null
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory == null)
            {
                Debug.LogError("PlayerInventory still not found when submitting guess!");
                return;
            }
        }

        string playerWord = string.Join("", playerInventory.inventory);
        guessWord = playerWord.ToUpper();

        Debug.Log("ObstaclePuzzle - Submitting guess: " + guessWord + " for puzzle: " + puzzleWord);
        uiManager.UpdateDictionaryGuesses(guessWord); // update the dictionary with the guessed word

        if (guessWord == puzzleWord)
        {
            Debug.Log("ObstaclePuzzle: Obstacle puzzle solved!");
            
            isPuzzleSolved = true;
            uiManager.UpdateDictionary(puzzleWord); // update the dictionary with the puzzle word 
            uiManager.StartCoroutine(uiManager.DictionaryUpdated());  // start DictionaryUpdated coroutine

            AudioManager.Instance.PlayPuzzleSolvedSound();

            Interact();
            
            // Play the solved puzzle sound if available
            if (puzzleSolvedClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(puzzleSolvedClip);
            }
        }
        else {
            Debug.Log("ObstaclePuzzle: Wrong answer. Try again.");
            uiManager.ShowWrongGuessUI();
        }
    }

    private IEnumerator ObstacleSolved()
    {
        Debug.Log("ObstaclePuzzle: dactivating obstacle...");
        // Disable the collider first so player can pass through
        if (obstacleCollider != null)
        {
            obstacleCollider.enabled = false;
        }
        
        // Wait for the delay
        yield return new WaitForSeconds(deactivationDelay);
        
        // Deactivate the visual part
        if (visualObject != null)
        {
            visualObject.SetActive(false);
        }
        // depending on the type of obstacle, can either disable, swap sprites, or play an andio clip
    }

    private void Deactivate(){
        Debug.Log("ObstaclePuzzle: Performing Deactivate action");
        
        // Change sprite if we have one
        if (spriteRenderer != null && deactivatedSprite != null)
        {
            spriteRenderer.sprite = deactivatedSprite;
        }
        
        // Disable the collider
        if (obstacleCollider != null)
        {
            obstacleCollider.enabled = false;
        }
        
        // Deactivate the specified object
        if (objectToDeactivate != null)
        {
            StartCoroutine(DeactivateAfterDelay(objectToDeactivate, deactivationDelay));
        }
        else
        {
            Debug.LogWarning("No object to deactivate specified on " + gameObject.name);
        }
    }

    private void Activate(){
        Debug.Log("ObstaclePuzzle: Performing Activate action");
        
        // Change sprite if we have one
        if (spriteRenderer != null && activatedSprite != null)
        {
            spriteRenderer.sprite = activatedSprite;
        }
        
        // Disable the collider if needed
        if (obstacleCollider != null)
        {
            obstacleCollider.enabled = false;
        }
        
        // Activate the specified object
        if (objectToActivate != null)
        {
            StartCoroutine(ActivateAfterDelay(objectToActivate, deactivationDelay));
        }
        else
        {
            Debug.LogWarning("No object to activate specified on " + gameObject.name);
        }        
    }

    private void PlayAudio(){
        Debug.Log("ObstaclePuzzle: Playing audio");
        
        // Play the audio clip
        if (audioSource != null && puzzleSolvedClip != null)
        {
            audioSource.clip = puzzleSolvedClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or puzzleSolvedClip not set on " + gameObject.name);
        }
        
        // Disable the collider if needed
        if (obstacleCollider != null)
        {
            obstacleCollider.enabled = false;
        }        
    }

     // Helper coroutines for delayed activation/deactivation
    private IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
    
    private IEnumerator ActivateAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}
