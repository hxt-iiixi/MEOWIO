using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quit_button : MonoBehaviour
{
    public GameObject Quit_button;

    public GameObject Quit_frame;

    public GameObject yes_button;

    public GameObject no_button;
    // Start is called before the first fra me update
    void Start()
    {
        Quit_button.SetActive(true);
        HideQuit();
    }
    public void OnQuitBtnClick()
    {
        Quit_frame.SetActive(true);
        yes_button.SetActive(true);
        no_button.SetActive(true);
    }
    public void HideQuit()
    {
        Quit_frame.SetActive(false);
        yes_button.SetActive(false);
        no_button.SetActive(false);
    }
    public void OnNoButton()
    {
        if (no_button != null)
        {
            HideQuit();
        }
    }
    public void OnYesButton()
    {
        Application.Quit();
    }
}
