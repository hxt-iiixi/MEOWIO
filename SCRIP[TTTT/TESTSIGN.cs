using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class SignUpManager : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_InputField emailField;
    public string phpUrl = "http://localhost/MEOWRDB/test-signin.php";  
    public Button signInButton;

    void Start()
    {
        signInButton.onClick.AddListener(OnSignUpButtonClicked);

        usernameField.onValueChanged.AddListener(delegate { ValidateInput(); });
        passwordField.onValueChanged.AddListener(delegate { ValidateInput(); });
        emailField.onValueChanged.AddListener(delegate { ValidateInput(); });
    }


    void ValidateInput()
    {

        if (!string.IsNullOrEmpty(usernameField.text) && !string.IsNullOrEmpty(passwordField.text) && !string.IsNullOrEmpty(emailField.text))
        {
            signInButton.interactable = true;
        }
        else
        {
            signInButton.interactable = false;
        }
    }
    public void OnSignUpButtonClicked()
    {
        StartCoroutine(SignUpUser());
    }

    private IEnumerator SignUpUser()
    {
     
        WWWForm form = new WWWForm();
        form.AddField("signInUser", usernameField.text);
        form.AddField("signInPass", passwordField.text);
        form.AddField("signInEmail", emailField.text);

        using (UnityWebRequest www = UnityWebRequest.Post(phpUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("New user created successfully"))
                {
                    Debug.Log("User registered successfully");

                    PlayerPrefs.SetString("username", usernameField.text);  

                    
                    SceneManager.LoadScene("USER");  
                }
                else if (www.downloadHandler.text.Contains("Username already taken"))
                {
                    Debug.Log("Username is already taken");
                }
                else
                {
                    Debug.Log("Registration failed");
                }
            }
        }
    }
}
