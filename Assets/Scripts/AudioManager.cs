using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource musicSource; // For background music
    [SerializeField] private AudioSource sfxSource;   // For sound effects

    [Header("Audio Clips")]
    [SerializeField] private AudioClip backgroundMusic;  // Background music
    [SerializeField] private AudioClip pickupSound;      // Pickup item sound
    [SerializeField] private AudioClip shootSound;       // Shoot sound effect

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

        DontDestroyOnLoad(gameObject);
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
            sfxSource.PlayOneShot(pickupSound); // Play sound once
        }
    }

    public void PlayShootSound()
    {
        if (sfxSource != null && shootSound != null)
        {
            sfxSource.PlayOneShot(shootSound); // Play sound once
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
