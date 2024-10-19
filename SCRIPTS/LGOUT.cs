using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;  

public class LogOutManager : MonoBehaviour
{
    public Button logOutButton;  

    void Start()
    {
        
        logOutButton.onClick.AddListener(OnLogOutButtonClicked);
    }

    public void OnLogOutButtonClicked()
    {

        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("levelsUnlocked"); // Remove username
        PlayerPrefs.Save();  // Make sure PlayerPrefs is saved

        Debug.Log("User logged out");



        SceneManager.LoadScene("USER");  
    }
}