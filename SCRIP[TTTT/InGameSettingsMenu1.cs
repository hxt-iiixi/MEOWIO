using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameSettingsMenu1 : MonoBehaviour
{
    public GameObject settings_button;     // Button to open settings
    public GameObject settingsCanvas;      // Canvas for all settings UI (On/Off, BGON/BGOFF, etc.)
    public GameObject[] Canvases;          // Other canvas elements in the scene
    public GameObject mainCamera;

    public GameObject RES;                 // Reference for the respawn location
    private GameObject player;
    private bool isPaused = false;         // Game is not paused at the start

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Ensure the Settings Canvas is hidden at the start
        settingsCanvas.SetActive(false);
    }

    // Show the pause menu
    public void ShowPause()
    {
        // Show the settings canvas
        settingsCanvas.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Hide the pause menu
    public void HidePauseMenu()
    {
        // Hide the settings canvas
        settingsCanvas.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Toggle pause on button click
    public void OnPauseButtonClick()
    {
        if (!isPaused)
        {
            ShowPause();
        }
        else
        {
            HidePauseMenu();
        }
    }

    // Resume the game
    public void OnResumeButtonClick()
    {
        HidePauseMenu();
    }

    // Restart button action: Move player to respawn point
    public void OnRestartButton()
    {
        if (player != null && RES != null)
        {
            // Move the player to the respawn position
            player.transform.position = RES.transform.position;
            HidePauseMenu();
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogError("Player or RES (respawn) is not assigned.");
        }
    }

    public void OnSettingButton()
    {
        SceneManager.LoadScene("SETTINGS"); // Going to settings scene
        HidePauseMenu();
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("MAIN"); // Going to main page
        HidePauseMenu();
    }
}
