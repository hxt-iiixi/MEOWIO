using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SIGNIN : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField emailInput;
    public Button signInButton;

    // Start is called before the first frame update
    void Start()
    {

        signInButton.onClick.AddListener(() => {
            StartCoroutine(Main.instance.Web.RegUser(usernameInput.text, passwordInput.text, emailInput.text));
        });

    }
}
