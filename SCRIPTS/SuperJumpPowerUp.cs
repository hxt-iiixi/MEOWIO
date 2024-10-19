using System.Collections;
using UnityEngine;

public class SuperJumpPowerUp : MonoBehaviour
{
    public float superJumpMultiplier = 2.0f;
    public float powerUpDuration = 3f;
    public AudioClip powerUpSound;
    private bool isCollected = false;

    private Catscript playerMovement;
    private AudioSource audioSource;

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Catscript>();
        audioSource = GetComponent<AudioSource>();
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

        gameObject.SetActive(false);

        yield return new WaitForSeconds(powerUpDuration);

        playerMovement.jumpPower = originalJumpPower;

        Debug.Log("Power-up effect ended. Jump power reset to original.");
    }

    public void ResetPowerUp()
    {
        isCollected = false;
        gameObject.SetActive(true);
    }
}
