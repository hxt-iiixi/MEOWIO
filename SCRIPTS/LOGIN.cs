using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LOGIN : MonoBehaviour
{
    public static TMP_InputField nameField;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;

    // Start is called before the first frame update
    void Start()
    {
       
        loginButton.onClick.AddListener(() => {
           StartCoroutine( Main.instance.Web.Login(usernameInput.text, passwordInput.text));
        });

    }
    void OnLoginButtonPressed()
    {
        string username = usernameInput.text; 
        string password = passwordInput.text; 

       
        
    }


}
