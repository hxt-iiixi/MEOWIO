using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LOGIN : MonoBehaviour
{
    public TMP_InputField usernameInput;  
    public TMP_InputField passwordInput;  
    public Button loginButton;            

    void Start()
    {
        
        loginButton.interactable = false;

        
        usernameInput.onValueChanged.AddListener(delegate { ValidateInput(); });
        passwordInput.onValueChanged.AddListener(delegate { ValidateInput(); });

       
        loginButton.onClick.AddListener(() => {
            StartCoroutine(Main.instance.Web.Login(usernameInput.text, passwordInput.text));
        });
    }

   
    void ValidateInput()
    {
       
        if (!string.IsNullOrEmpty(usernameInput.text) && !string.IsNullOrEmpty(passwordInput.text))
        {
            loginButton.interactable = true;  
        }
        else
        {
            loginButton.interactable = false; 
        }
    }

}
