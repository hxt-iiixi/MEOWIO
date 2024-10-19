using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spike_code : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Gameover;
    public GameObject yes_button;
    public GameObject no_button;
    public GameObject try_again;

    public AudioClip gameOverSound;
    private AudioSource audioSource;

    public GameObject respawn;
    private GameObject player;
    private Animator spike;
    
    void Start()
    {
        spike = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (yes_button != null)
        {
            Button yesBt = yes_button.GetComponent<Button>();
            yesBt.onClick.AddListener(OnYesButtonClick);
        }
        if (no_button != null)
        {
            Button noBt = no_button.GetComponent<Button>();
            noBt.onClick.AddListener(OnNoButton);
        }
        audioSource = GetComponent<AudioSource>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Catscript catmove = other.GetComponent<Catscript>();
            Rigidbody2D cat_Rb = other.GetComponent<Rigidbody2D>();
            if (catmove != null && cat_Rb != null)
            {
                catmove.moveSpeed = 0f;
                catmove.jumpPower = 0f;

                cat_Rb.velocity = Vector2.zero;
                cat_Rb.angularVelocity = 0f;
                cat_Rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                if (Gameover != null) Gameover.SetActive(true);
                if (try_again != null) try_again.SetActive(true);
                if (yes_button != null) yes_button.SetActive(true);
                if (no_button != null) no_button.SetActive(true);

                if (spike != null)
                {
                    spike.enabled = false;
                }

                if (gameOverSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(gameOverSound);
                }

            }
        }
    }


    void OnYesButtonClick()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        if (player != null && respawn != null)
        {
            // Move the player to the respawn position
            player.transform.position = respawn.transform.position;

            // Hide game over UI elements
            if (Gameover != null) Gameover.SetActive(false);
            if (try_again != null) try_again.SetActive(false);
            if (yes_button != null) yes_button.SetActive(false);
            if (no_button != null) no_button.SetActive(false);

            // Reset player's movement and jump power
            Catscript catmove = player.GetComponent<Catscript>();
            Rigidbody2D cat_Rb = player.GetComponent<Rigidbody2D>();

            if (catmove != null && cat_Rb != null)
            {
                catmove.moveSpeed = 3f;  
                catmove.jumpPower = 5f;  

                cat_Rb.constraints = RigidbodyConstraints2D.None;
                cat_Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

           
            if (spike != null)
            {
                spike.enabled = true;
            }

            
            SuperJumpPowerUp[] jumpPowerUps = FindObjectsOfType<SuperJumpPowerUp>();
            foreach (SuperJumpPowerUp powerUp in jumpPowerUps)
            {
                powerUp.gameObject.SetActive(true);  
                powerUp.ResetPowerUp();             
            }

            SuperSpeedPowerUp[] speedPowerUps = FindObjectsOfType<SuperSpeedPowerUp>();
            foreach (SuperSpeedPowerUp powerUp in speedPowerUps)
            {
                powerUp.gameObject.SetActive(true);  
                powerUp.ResetPowerUp();              
            }
        }
    }

    void OnNoButton()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVELS");
    }
    
}