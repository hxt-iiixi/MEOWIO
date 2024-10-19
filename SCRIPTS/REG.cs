using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;



public class REG : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField emailInput;
    public Button signInButton;


    public void CallReg()
    {
        StartCoroutine(Regis());
    }
    IEnumerator Regis()
    {
        WWWForm form = new WWWForm();
        form.AddField("signInUser", usernameInput.text);
        form.AddField("signInPass", passwordInput.text);
        form.AddField("signInEmail", emailInput.text);
        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/MEOWRDB/reg.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("USER NABUHAY");
            
        }
        else
        {
            Debug.Log("USER DI NABUHAY. Error #" + www.result);
        }
    }
   
}
