using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public GameObject playerInfoContainer;
    public GameObject playerInfoTemplate;
    void Start()
    {
        StartCoroutine(GetRequest("http://localhost/MEOWRDB/lead.php"));
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

                    string rawresponse = webRequest.downloadHandler.text;

                    string[] users = rawresponse.Split("*");
                    for (int i = 0; i < users.Length; i++)
                    {
                        if (users[i] != "")
                        {
                            string[] usersinfo = users[i].Split(",");
                            Debug.Log("Name: " + usersinfo[0] + " Level: " + usersinfo[1]);
                            GameObject gobj = (GameObject)Instantiate(playerInfoTemplate);
                            gobj.transform.SetParent(playerInfoContainer.transform, false); // Set the second parameter to false for correct scaling.
                            gobj.transform.localScale = Vector3.one; // Ensure the scale is reset to 1.
                            gobj.transform.localPosition = Vector3.zero;
                            gobj.transform.SetParent(playerInfoContainer.transform);
                            gobj.GetComponent<LINFO>().username.text = usersinfo[0];
                            gobj.GetComponent<LINFO>().level.text = usersinfo[1];
                       

                        }


                    }
                    break;
            }
        }
    }
}