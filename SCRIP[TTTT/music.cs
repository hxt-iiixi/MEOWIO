using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    public static music Instance;  // Singleton instance for the music controller
    private AudioSource musicSource;  // The AudioSource that plays the background music
    private bool muted = false;  // State to track if the music is muted

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persist the music object across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
            return;
        }

        musicSource = GetComponent<AudioSource>();  // Get the AudioSource for background music
        if (musicSource == null)
        {
            Debug.LogError("No AudioSource found on the music gameObject! Please attach one.");
        }

        LoadMusicSettings();  // Load saved music mute settings when the game starts
    }

    // Method to toggle the background music between mute and unmute
    public void ToggleMusic()
    {
        muted = !muted;  // Flip the mute state

        if (muted)
        {
            musicSource.mute = true;  // Mute the background music
            musicSource.Stop();  // Stop playing the music
        }
        else
        {
            musicSource.mute = false;  // Unmute the background music
            musicSource.time = 0;  // Restart music from the beginning
            musicSource.Play();  // Play the background music
        }

        // Save the new mute state (1 = muted, 0 = unmuted)
        PlayerPrefs.SetInt("MusicMuted", muted ? 1 : 0);
        PlayerPrefs.Save();  // Save the preference to disk
    }

    // Method to load the saved music mute state from PlayerPrefs
    private void LoadMusicSettings()
    {
        muted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;  // Load the mute state (1 = muted, 0 = unmuted)
        musicSource.mute = muted;  // Apply the mute state to the AudioSource

        if (!muted)
        {
            musicSource.Play();  // If music is not muted, start playing it
        }
    }
}
