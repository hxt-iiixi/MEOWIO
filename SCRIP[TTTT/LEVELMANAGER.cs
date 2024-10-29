using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class LEVELMANAGER : MonoBehaviour
{
    public AudioClip lockedLevelSound;   
    public AudioClip unlockedLevelSound; 
    private AudioSource audioSource;
    public Button[] levelButtons;  
    private int levelsUnlocked = 1;    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FetchLevelsCompleted());
    }

    
    IEnumerator FetchLevelsCompleted()
    {
      
        string username = PlayerPrefs.GetString("username", "");
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("Username not found in PlayerPrefs");
            yield break;
        }

        
        WWWForm form = new WWWForm();
        form.AddField("user_name", username);

      
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MEOWRDB/getLevelsCompleted.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching levels: " + www.error);
            }
            else
            {
             
                int fetchedLevels = int.Parse(www.downloadHandler.text);
                levelsUnlocked = fetchedLevels;

               
                PlayerPrefs.SetInt("levelsUnlocked", levelsUnlocked);
                PlayerPrefs.Save();

               
                SetupLevelButtons();
            }
        }
    }

   
    void SetupLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelsUnlocked)
            {
               
                levelButtons[i].interactable = false;
            }
            else
            {
               
                int levelIndex = i + 1;
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
            }
        }
    }

    
    public void LoadLevel(int levelIndex)
    {
 

        SceneManager.LoadScene("Level" + levelIndex);
    }

   
    public void CompleteLevel(int levelIndex)
    {
        if (levelIndex >= levelsUnlocked)
        {
          
            levelsUnlocked = levelIndex;
            PlayerPrefs.SetInt("levelsUnlocked", levelsUnlocked);
            PlayerPrefs.Save();
        }
    }
}
