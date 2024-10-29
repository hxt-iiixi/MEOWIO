using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finishline : MonoBehaviour
{
    public GameObject youwin;
    public GameObject tap_continue;
    public GameObject Back_button;
    public GameObject Next_button;
    public GameObject nextlevel;
    private GameObject player;

    public AudioClip gameWinSound;
    private AudioSource audioSource;

    private bool tap_screen = false;
    private bool playerReachedFinish = false;
    public int currentLevelIndex = 1;
    public int coinsEarned = 10;
    public GameObject pause;

    private int userLevel = 0; // Store the player's current user level

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Back_button != null)
        {
            Button yesBt = Back_button.GetComponent<Button>();
            yesBt.onClick.AddListener(OnBack_buttonClick);
        }
        if (Next_button != null)
        {
            Button noBt = Next_button.GetComponent<Button>();
            noBt.onClick.AddListener(OnNext_button);
        }
        audioSource = GetComponent<AudioSource>();

        // Fetch user's current level from the database before proceeding
        StartCoroutine(GetUserLevelFromDatabase());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Catscript catMovement = other.GetComponent<Catscript>();
            Rigidbody2D catRb = other.GetComponent<Rigidbody2D>();

            if (catMovement != null && catRb != null)
            {
                catMovement.moveSpeed = 0f;
                catMovement.jumpPower = 0f;
                catRb.velocity = Vector2.zero;
                catRb.angularVelocity = 0f;
                catRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                GameObject movingPlatform = GameObject.FindGameObjectWithTag("MovingGround");
                if (movingPlatform != null)
                {
                    Animator platformAnimator = movingPlatform.GetComponent<Animator>();
                    if (platformAnimator != null)
                    {
                        platformAnimator.speed = 0f;  // Pauses the animation of the moving ground
                    }
                }

                if (youwin != null) youwin.SetActive(true);
                if (tap_continue != null) tap_continue.SetActive(true);

                playerReachedFinish = true;
                pause.SetActive(false);
                if (gameWinSound != null)
                {
                    sound.Instance.PlaySound(gameWinSound);
                }

                // Only unlock the next level and update the database if the user hasn't completed this level
                if (currentLevelIndex > userLevel)
                {
                    UnlockNextLevel();
                    AddCoinsToDatabase();
                }
                else
                {
                    Debug.Log("User has already completed this level. No update needed.");
                }
            }
        }
    }

    void Update()
    {
        if (playerReachedFinish && Input.GetMouseButtonDown(0) && !tap_screen)
        {
            if (youwin != null) youwin.SetActive(false);
            if (tap_continue != null) tap_continue.SetActive(false);

            if (nextlevel != null) nextlevel.SetActive(true);
            if (Next_button != null) Next_button.SetActive(true);
            if (Back_button != null) Back_button.SetActive(true);

            tap_screen = true;
        }
    }

    void OnBack_buttonClick()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVELS");
    }

    void OnNext_button()
    {
        int nextLevelIndex = currentLevelIndex;
        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        if (nextLevelIndex <= levelsUnlocked)
        {
            SceneManager.LoadSceneAsync("LEVEL " + nextLevelIndex);
        }
        else
        {
            Debug.LogWarning("Next level is locked!");
        }
    }

    void UnlockNextLevel()
    {
        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        if (currentLevelIndex >= levelsUnlocked)
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevelIndex);
            PlayerPrefs.Save();
        }
    }

    void AddCoinsToDatabase()
    {
        StartCoroutine(SendCoinsData());
    }

    // Fetch user_level from the database before making any updates
    IEnumerator GetUserLevelFromDatabase()
    {
        WWWForm form = new WWWForm();
        string username = PlayerPrefs.GetString("username", "");
        form.AddField("user_name", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MEOWRDB/getUserLevel.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                // Parse userLevel from the server response
                int.TryParse(www.downloadHandler.text, out userLevel);
                Debug.Log("Current user level from database: " + userLevel);
            }
        }
    }

    IEnumerator SendCoinsData()
    {
        WWWForm form = new WWWForm();
        string username = PlayerPrefs.GetString("username", "");
        Debug.Log("Sending User Name: " + username);

        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("User Name is empty!");
            yield break;
        }

        form.AddField("user_name", username);
        form.AddField("coins", coinsEarned);
        form.AddField("level", currentLevelIndex);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/MEOWRDB/updateCoins.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Response from server: " + www.downloadHandler.text);
            }
        }
    }
}
