using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public GameObject musicOnButton;
    public GameObject musicOffButton;

    void Start()
    {
        UpdateMusicToggleUI(); // Ensure the UI matches the saved settings when the scene loads
    }

    public void ToggleMusic()
    {
        // Toggle the music state in the Music script
        if (music.Instance != null)
        {
            music.Instance.ToggleMusic();
        }

        UpdateMusicToggleUI(); // Update the UI to reflect changes
    }

    private void UpdateMusicToggleUI()
    {
        // Ensure we load the correct music state from PlayerPrefs
        bool isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;

        // Set the button visibility based on the muted state
        musicOnButton.SetActive(!isMuted);  // Show "On" button when music is playing
        musicOffButton.SetActive(isMuted);  // Show "Off" button when music is muted
    }
}
