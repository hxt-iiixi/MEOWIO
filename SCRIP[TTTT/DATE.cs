using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class DATE : MonoBehaviour
{
    public GameObject successPopup;  
    public GameObject failurePopup;   
    public Button successButton;     
    public Button failureButton;

    public 
    // Start is called before the first frame update
    void Start()
    {
        successPopup.SetActive(false);
        failurePopup.SetActive(false);

       
        successButton.onClick.AddListener(OnSuccessButtonClick);
        failureButton.onClick.AddListener(OnFailureButtonClick);

        // A correct website page.
        //StartCoroutine(GetRequest("http://localhost/MEOWRDB/users.php"));
        // A non-existing page.
        //StartCoroutine(GetRequest("http://localhost/MEOWRDB/users.php"));

        // StartCoroutine(Login("test2", "1234"));
        //StartCoroutine(RegUser("rizaTest", "1234", "rizaTest@gmail.com"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    public IEnumerator Login(string user_name, string user_password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", user_name);
        form.AddField("loginPass", user_password);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/MEOWRDB/login.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);

                       
            failurePopup.SetActive(true);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log("Response: " + response);

            if (response.Contains("Success")) 
            {
              
                successPopup.SetActive(true);
                PlayerPrefs.SetString("username", user_name);
            }
            else
            {
                
                failurePopup.SetActive(true);
            }
        }

    }
    void OnSuccessButtonClick()
    {
       
        successPopup.SetActive(false);
        SceneManager.LoadScene("USER");
    }

    void OnFailureButtonClick()
    {
        
        failurePopup.SetActive(false);
        SceneManager.LoadScene("LOG IN");
    }
    public IEnumerator RegUser(string user_name, string user_password, string user_email)
    {
        WWWForm form = new WWWForm();
        form.AddField("signInUser", user_name);
        form.AddField("signInPass", user_password);
        form.AddField("signInEmail", user_email);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/MEOWRDB/redgUser.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
           
        }
    }
    
}
