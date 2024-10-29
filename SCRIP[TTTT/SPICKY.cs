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
    public Animator mPLat;
    public GameObject respawn;
    private GameObject player;
    private Animator spike;
    public GameObject pause;

    void Start()
    {
        Debug.Log("Spike script Start called");
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
                // Stop the player's movement
                catmove.moveSpeed = 0f;
                catmove.jumpPower = 0f;

                cat_Rb.velocity = Vector2.zero;
                cat_Rb.angularVelocity = 0f;
                cat_Rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                // Stop the moving platform animation
                GameObject movingPlatform = GameObject.FindGameObjectWithTag("MovingGround");
                if (movingPlatform != null)
                {
                    Animator platformAnimator = movingPlatform.GetComponent<Animator>();
                    if (platformAnimator != null)
                    {
                        platformAnimator.speed = 0f;  // Pauses the animation of the moving ground
                    }
                }

           

                // Show the Game Over UI
                if (Gameover != null) Gameover.SetActive(true);
                if (try_again != null) try_again.SetActive(true);
                if (yes_button != null) yes_button.SetActive(true);
                if (no_button != null) no_button.SetActive(true);
                pause.SetActive(false);
                // Disable the spike animation
                if (spike != null)
                {
                    Animator platformAnimator = spike.GetComponent<Animator>();
                    if (platformAnimator != null)
                    {
                        platformAnimator.speed = 0f;  // Resumes the moving platform animation
                    }
                }

                // Play the game over sound
                if (gameOverSound != null)
                {
                    sound.Instance.PlaySound(gameOverSound);  // Replace with your AudioClip logic
                }
            }
        }
    }


    void OnYesButtonClick()
    {
        Debug.Log("Yes button clicked. Restarting game.");

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
            pause.SetActive(true);
            if (spike != null)
            {
                Animator platformAnimator = spike.GetComponent<Animator>();
                if (platformAnimator != null)
                {
                    platformAnimator.speed = 1f;  // Resumes the moving platform animation
                }
            }
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

            // Resume the moving ground animation
            GameObject movingPlatform = GameObject.FindGameObjectWithTag("MovingGround");
            if (movingPlatform != null)
            {
                Animator platformAnimator = movingPlatform.GetComponent<Animator>();
                if (platformAnimator != null)
                {
                    platformAnimator.speed = 1f;  // Resumes the moving platform animation
                }
            }

            


            // Reset power-ups
            SuperJumpPowerUp[] jumpPowerUps = FindObjectsOfType<SuperJumpPowerUp>();
            foreach (SuperJumpPowerUp powerUp in jumpPowerUps)
            {
                powerUp.ResetPowerUp();
            }

            SuperSpeedPowerUp[] speedPowerUps = FindObjectsOfType<SuperSpeedPowerUp>();
            foreach (SuperSpeedPowerUp powerUp in speedPowerUps)
            {
                powerUp.ResetPowerUp();
            }
        }
        else
        {
            Debug.LogError("Player or respawn position is missing!");
        }
    }


    void OnNoButton()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVELS");
    }
    
}