using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Gamemanager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetUserProgress());
        UnlockLevelsOnStart();
    }

    IEnumerator GetUserProgress()
    {
        string username = PlayerPrefs.GetString("username", "");
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("User Name is empty!");
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("user_name", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MEOWRDB/getUserProgress.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log("Response from server: " + response);

                UserProgress progress = JsonUtility.FromJson<UserProgress>(response);
                PlayerPrefs.SetInt("levelsUnlocked", progress.level);
                PlayerPrefs.SetInt("coins", progress.coins);
                PlayerPrefs.Save();
            }
        }
    }

    void UnlockLevelsOnStart()
    {
        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for (int i = 2; i <= levelsUnlocked; i++)
        {
            // Unlock level i in your UI
            Debug.Log("Unlocking Level " + i);
        }
    }

    [System.Serializable]
    public class UserProgress
    {
        public int level;
        public int coins;
    }
}
