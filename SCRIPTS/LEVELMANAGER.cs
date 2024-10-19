using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class LEVELMANAGER : MonoBehaviour
{
    public Button[] levelButtons;  // Array of level buttons
    private int levelsUnlocked = 0;    // Number of levels unlocked (default to 1)

    void Start()
    {
        // Fetch the levels completed from the server
        StartCoroutine(FetchLevelsCompleted());
    }

    // Coroutine to fetch levels completed from the database
    IEnumerator FetchLevelsCompleted()
    {
        // Assuming the username is stored in PlayerPrefs
        string username = PlayerPrefs.GetString("username", "");
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("Username not found in PlayerPrefs");
            yield break;
        }

        // Create a form and add the username as a parameter
        WWWForm form = new WWWForm();
        form.AddField("user_name", username);

        // Send request to server to get the levels completed
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MEOWRDB/getLevelsCompleted.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching levels: " + www.error);
            }
            else
            {
                // Assuming the server returns the number of levels completed as an integer
                int fetchedLevels = int.Parse(www.downloadHandler.text);
                levelsUnlocked = fetchedLevels;

                // Update PlayerPrefs with the fetched levels
                PlayerPrefs.SetInt("levelsUnlocked", levelsUnlocked);
                PlayerPrefs.Save();

                // Call a method to set up the level buttons based on levelsUnlocked
                SetupLevelButtons();
            }
        }
    }

    // Set up level buttons based on the number of levels unlocked
    void SetupLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelsUnlocked)
            {
                // If the level is locked, disable the button
                levelButtons[i].interactable = false;
            }
            else
            {
                // Level is unlocked, so add a listener to load the level when clicked
                int levelIndex = i + 1;
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
            }
        }
    }

    // This function is called when a player selects a level
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    // This function should be called when the player completes a level
    public void CompleteLevel(int levelIndex)
    {
        if (levelIndex >= levelsUnlocked)
        {
            // Unlock the next level
            levelsUnlocked = levelIndex;
            PlayerPrefs.SetInt("levelsUnlocked", levelsUnlocked);
            PlayerPrefs.Save();
        }
    }
}
