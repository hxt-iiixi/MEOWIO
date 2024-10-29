using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class aboutsetting : MonoBehaviour
{
    public GameObject aboutFrame;
    public GameObject titleAbout;
    public GameObject titleAbout1;
    public GameObject textTMP;
    public GameObject xButton;
    public Button Backbutton;

    public Button Helpbutton;

    // ipakita yung about pag pinindot
    public void ShowAbout()
    {
        aboutFrame.SetActive(true);
        titleAbout.SetActive(true);
        titleAbout1.SetActive(true);
        textTMP.SetActive(true);
        xButton.SetActive(true);
        Backbutton.interactable = false;
        Helpbutton.interactable = false;
    }

    //itago yung mga para dinamapindot
    public void HideAbout()
    {
        aboutFrame.SetActive(false);
        titleAbout.SetActive(false);
        titleAbout1.SetActive(false);
        textTMP.SetActive(false);
        xButton.SetActive(false);
        Backbutton.interactable = true;
        Helpbutton.interactable = true;
    }
}
