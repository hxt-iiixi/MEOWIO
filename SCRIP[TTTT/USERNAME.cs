using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class USERNAME : MonoBehaviour
{
    public TMP_Text usernameText;

    void Start()
    {
        UpdateUsernameDisplay();  // Ensure username is set correctly on start
    }

    public void UpdateUsernameDisplay()
    {
        // Fetch the username from PlayerPrefs, default to "Guest" if not found
        string username = PlayerPrefs.GetString("username", "Guest");
        usernameText.text = username;
    }
}