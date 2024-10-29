using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class help : MonoBehaviour
{
    public GameObject helpframe;

    public GameObject Gamemech;

    public GameObject GameDesc;

    public GameObject Xbutton;

    public Button Backbutton;

    public Button AboutButton;


    // Start is called before the first frame update
    public void showhelp()
    {
        helpframe.SetActive(true);
        Gamemech.SetActive(true);
        GameDesc.SetActive(true);
        Xbutton.SetActive(true);
        Backbutton.interactable = false;
        AboutButton.interactable = false;
    }

    // Update is called once per frame
    public void hidehelp()
    {
        helpframe.SetActive(false);
        Gamemech.SetActive(false);
        GameDesc.SetActive(false);
        Xbutton.SetActive(false);
        Backbutton.interactable = true;
        AboutButton.interactable = true;
    }
}
