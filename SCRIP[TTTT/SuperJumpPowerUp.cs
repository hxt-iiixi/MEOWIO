using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuperJumpPowerUp : MonoBehaviour
{
    public float superJumpMultiplier = 2.0f;
    public float powerUpDuration = 3f;
    public AudioClip powerUpSound;
    private bool isCollected = false;

    private Catscript playerMovement;
    private AudioSource audioSource;
    public Image powerUpImage;
    public TMP_Text text;
    public GameObject powerup;
    private Collider2D powerUpCollider;
    public Vector3 originalPosition;  
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Catscript>();
        audioSource = GetComponent<AudioSource>();
        powerUpImage.enabled = true;
        text.enabled = false;
        originalPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            StartCoroutine(ActivateSuperJump());
        }
    }

    IEnumerator ActivateSuperJump()
    {
        if (audioSource != null && powerUpSound != null)
        {
            audioSource.PlayOneShot(powerUpSound);
        }

        float originalJumpPower = playerMovement.jumpPower;
        playerMovement.jumpPower *= superJumpMultiplier;

        Debug.Log("Power-up collected! Jump power increased.");

        text.enabled = true;
        powerUpImage.enabled = false;

        yield return new WaitForSeconds(powerUpDuration);
        text.enabled = false;
        playerMovement.jumpPower = originalJumpPower;

        Debug.Log("Power-up effect ended. Jump power reset to original." );
    }

    public void ResetPowerUp()
    {

        isCollected = false;  // Reset the collection status
        powerUpImage.enabled = true;
        text.enabled = false;

    }
}
