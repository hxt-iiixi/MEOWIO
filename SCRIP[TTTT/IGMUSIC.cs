using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGMUSIC : MonoBehaviour
{
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    private bool isOn;

    void Start()
    {

        UpdateMusicToggleUI();

        // Load the current mute state from PlayerPrefs
        isOn = PlayerPrefs.GetInt("MusicMuted", 0) == 0;
    }

    // Call this method every time the settings menu is opened
    public void UpdateMusicStateOnSettingsOpen()
    {
        // Load the current mute state from PlayerPrefs
        isOn = PlayerPrefs.GetInt("MusicMuted", 0) == 0;
        UpdateMusicToggleUI(); // Update the buttons based on the current music state
    }

    public void ToggleMusic()
    {
        // Toggle the music state in the Music script
        if (music.Instance != null)
        {
            music.Instance.ToggleMusic();
        }

        // Flip the toggle state and save it
        isOn = !isOn;

        // Save the new toggle state (0 for On, 1 for Off)
        PlayerPrefs.SetInt("MusicMuted", isOn ? 0 : 1);
        PlayerPrefs.Save(); // Save sound state

        // Update the UI to reflect the change
        UpdateMusicToggleUI();
    }

    private void UpdateMusicToggleUI()
    {
        // Ensure we load the correct music state from PlayerPrefs
        bool isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;

        // Show buttons only when the settings are open
        musicOnButton.SetActive(!isMuted);  // Show "On" button if the music is playing
        musicOffButton.SetActive(isMuted);  // Show "Off" button if the music is muted
    }
}
