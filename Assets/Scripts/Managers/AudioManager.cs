using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource musicSource; // For background music
    [SerializeField] private AudioSource sfxSource;   // For sound effects

    [Header("Audio Clips")]
    [SerializeField] private AudioClip backgroundMusic;  // Background music
    [SerializeField] private AudioClip pickupSound;      // Pickup item sound
    [SerializeField] private AudioClip clearInventorySound; // when inv is cleared at clearcrate
    [SerializeField] private AudioClip winSound;      // when clearing a level
    [SerializeField] private AudioClip hintSound;     // when showing the int UI
    [SerializeField] private AudioClip addToDictionarySound;    // when a word is added to the dictionary
    [SerializeField] private AudioClip openDictionarySound;    // when dictionary is opened
    [SerializeField] private AudioClip correctGuessSound;    // when a guess is right and submitted
    [SerializeField] private AudioClip wrongGuessSound;    // when a guess is wrong and submitted
    [SerializeField] private AudioClip puzzleSolvedSound;    // when a puzzle is solved correctly

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }

        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true; // Loop the background music
            musicSource.Play();
        }
    }

    public void PlayPickupSound()
    {
        if (sfxSource != null && pickupSound != null)
        {
            sfxSource.PlayOneShot(pickupSound); 
        }
    }
    public void PlayClearInventorySound()
    {
        if (sfxSource != null && pickupSound != null)
        {
            sfxSource.PlayOneShot(clearInventorySound); 
        }
    }

    public void PlayWinSound()
    {
        if (sfxSource != null && winSound != null)
        {
            sfxSource.PlayOneShot(winSound); 
        }
    }

    public void PlayHintSound()
    {
        if (sfxSource != null && hintSound != null)
        {
            sfxSource.PlayOneShot(hintSound); 
        }
    }
    
    public void PlayAddToDictionarySound()
    {
        if (sfxSource != null && addToDictionarySound != null)
        {
            sfxSource.PlayOneShot(addToDictionarySound); 
        }
    }
    public void PlayOpenDictionarySound()
    {
        if (sfxSource != null && openDictionarySound != null)
        {
            sfxSource.PlayOneShot(openDictionarySound); 
        }
    }
    public void PlayCorrectGuessSound()
    {
        if (sfxSource != null && correctGuessSound != null)
        {
            sfxSource.PlayOneShot(correctGuessSound); 
        }
    }
    public void PlayWrongGuessSound()
    {
        if (sfxSource != null && wrongGuessSound != null)
        {
            sfxSource.PlayOneShot(wrongGuessSound); 
        }
    }
    public void PlayPuzzleSolvedSound()
    {
        if (sfxSource != null && puzzleSolvedSound != null)
        {
            sfxSource.PlayOneShot(puzzleSolvedSound); 
        }
    }

    // Optional: Volume control
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
