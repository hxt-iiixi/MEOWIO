using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sound : MonoBehaviour
{
    public static sound Instance;
    private bool Muted = false;
    private List<AudioSource> soundEffects = new List<AudioSource>();
    private AudioSource mainAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure sound manager persists across scenes

            CacheAudioSources();

            mainAudioSource = GetComponent<AudioSource>();
            if (mainAudioSource == null)
            {
                mainAudioSource = gameObject.AddComponent<AudioSource>(); // Add main AudioSource if missing
            }
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Load the saved mute state from PlayerPrefs
        Muted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateSound(); // Apply mute state to all cached sound sources
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Handle scene loads
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Remove listener when disabled
    }

    private void CacheAudioSources()
    {
        // Cache all AudioSources that are not tagged as "Music"
        soundEffects.Clear();
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            if (!audioSource.CompareTag("Music"))
            {
                soundEffects.Add(audioSource); // Add non-music audio sources to the list
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Refresh cached audio sources when the scene is loaded
        CacheAudioSources();
        UpdateSound(); // Ensure correct mute state is applied
    }

    public void PlaySound(AudioClip clip)
    {
        // Play a sound if not muted
        if (!Muted && clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position); // Play at camera position
        }
    }

    public void ToggleSound()
    {
        // Toggle the muted state and save it in PlayerPrefs
        Muted = !Muted;
        PlayerPrefs.SetInt("Muted", Muted ? 1 : 0); // Save the mute state (1 = muted, 0 = unmuted)
        PlayerPrefs.Save();

        UpdateSound(); // Update the mute state for all cached audio sources
    }

    private void UpdateSound()
    {
        // Apply the mute state to all cached sound effects
        foreach (AudioSource audioSource in soundEffects)
        {
            if (audioSource != null)
            {
                audioSource.mute = Muted; // Mute or unmute the audio source
            }
        }
    }
}
