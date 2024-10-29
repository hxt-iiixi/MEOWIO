using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public static BackgroundMusicController Instance;  // Singleton instance to ensure only one exists
    public AudioSource backgroundMusicSource;  // The AudioSource playing the background music

    private bool isMuted;  // Track if the music is muted

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists across all scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object alive across all scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Load the mute state from PlayerPrefs (0 = not muted, 1 = muted)
        isMuted = PlayerPrefs.GetInt("BackgroundMusicMuted", 0) == 1;

        // Update sound state based on saved mute state
        UpdateMusicState();
    }

    // Toggle between mute and unmute (this will be called from the Settings scene)
    public void ToggleMusicState()
    {
        isMuted = !isMuted;

        // Update the mute state of the background music
        UpdateMusicState();

        // Save the state to PlayerPrefs (1 = muted, 0 = not muted)
        PlayerPrefs.SetInt("BackgroundMusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Apply the mute state to the background music
    private void UpdateMusicState()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.mute = isMuted;
        }
    }
}
