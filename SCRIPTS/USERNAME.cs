using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class USERNAME : MonoBehaviour
{
    public TMP_Text usernameText;  // Drag your TextMeshPro text object here in the Inspector

    void Start()
    {
        // NEW: Retrieve the username from PlayerPrefs
        string username = PlayerPrefs.GetString("username", "Guest");  // Default value is "Guest" (NEW)

        // NEW: Display the username in the TextMeshPro text
        usernameText.text =  username;  // (NEW)
    }
}