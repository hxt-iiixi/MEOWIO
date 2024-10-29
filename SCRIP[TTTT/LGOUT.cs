using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogOutManager : MonoBehaviour
{
    public Button logInButton;   
    public Button logOutButton;  

    void Start()
    {
        
        if (PlayerPrefs.HasKey("username"))
        {
           
            logInButton.gameObject.SetActive(false); 
            logOutButton.gameObject.SetActive(true); 

           
            logOutButton.onClick.AddListener(OnLogOutButtonClicked);
        }
        else
        {
           
            logInButton.gameObject.SetActive(true);  
            logOutButton.gameObject.SetActive(false); 

           
            logInButton.onClick.AddListener(OnLogInButtonClicked);
        }
    }

    
    public void OnLogOutButtonClicked()
    {
       
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("levelsUnlocked");

       
        PlayerPrefs.Save();

       
        if (!PlayerPrefs.HasKey("username"))
        {
            Debug.Log("Username successfully cleared");
        }
        else
        {
            Debug.LogError("Failed to clear username");
        }

        
        logInButton.gameObject.SetActive(true); 
        logOutButton.gameObject.SetActive(false); 

        
        SceneManager.LoadScene("MAIN");
    }

  
    public void OnLogInButtonClicked()
    {
       
        SceneManager.LoadScene("LOGIN");
    }
}
