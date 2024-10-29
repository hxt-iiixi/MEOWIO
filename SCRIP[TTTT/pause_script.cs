using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pause_button : MonoBehaviour
{
    public GameObject Pause_button; 
    public GameObject Pause_res;
    public GameObject Restart_button;
    public GameObject Pause_frame;
    public GameObject Setting_button;
    public GameObject Resume_button;
    public GameObject Mmenu_button;
    public GameObject[] Canvases;
    public GameObject mainCamera;

    public GameObject RES; // Ensure this is assigned in the Inspector
    private GameObject player;
    private bool isPaused = false; // The game is not paused at the start

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Pause_button.SetActive(true);
        HidePauseMenu();

        if (Restart_button != null)
        {
            Button restartbtn = Restart_button.GetComponent<Button>();
            if (restartbtn != null)
            {
                restartbtn.onClick.AddListener(OnRestartButton);
            }
        }
    }

    // Show the pause menu
    public void ShowPause()
    {
        Restart_button.SetActive(true);
        Pause_res.SetActive(true);
        Pause_frame.SetActive(true);
        Setting_button.SetActive(true);
        Resume_button.SetActive(true);
        Mmenu_button.SetActive(true);
        foreach (GameObject canvasObj in Canvases)
        {
            Canvas canvas = canvasObj.GetComponent<Canvas>();

            // Ensure that only the canvases are hidden, not the main camera
            if (canvas != null && canvasObj != mainCamera)
            {
                canvasObj.SetActive(false);  // Hide the UI elements
            }
        }
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Hide the pause menu
    public void HidePauseMenu()
    {
        Restart_button.SetActive(false);
        Pause_frame.SetActive(false);
        Setting_button.SetActive(false);
        Resume_button.SetActive(false);
        Mmenu_button.SetActive(false);
        Pause_res.SetActive(false);
        foreach (GameObject canvasObj in Canvases)
        {
            Canvas canvas = canvasObj.GetComponent<Canvas>();

            // Ensure that only the canvases are shown, not the main camera
            if (canvas != null && canvasObj != mainCamera)
            {
                canvasObj.SetActive(true);  // Show the UI elements
            }
        }

        Time.timeScale = 1f; // Resume the game
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
        // Move the player to the respawn position
        player.transform.position = RES.transform.position;
        HidePauseMenu();
        Time.timeScale = 1f;
    }

    public void OnSettingButton()
    {
        SceneManager.LoadScene("SETTINGS"); // going to setting
        HidePauseMenu();
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("MAIN"); // going to main page
        HidePauseMenu();
    }

}