using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuperSpeedPowerUp : MonoBehaviour
{
    public float superSpeedMultiplier = 2.0f; 
    public float powerUpDuration = 5.0f;      
    public AudioClip powerUpSound;
    private bool isCollected = false;

    private Catscript playerMovement;          
    private AudioSource audioSource;
    public Image powerUpImage;
    public TMP_Text text;
    private Collider2D powerUpCollider;
    public GameObject powerup;
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
            StartCoroutine(ActivateSuperSpeed());
        }
    }

    IEnumerator ActivateSuperSpeed()
    {
       
        if (audioSource != null && powerUpSound != null)
        {
            audioSource.PlayOneShot(powerUpSound);
        }

       
        float originalSpeed = playerMovement.moveSpeed;
        playerMovement.moveSpeed *= superSpeedMultiplier;

        Debug.Log("Power-up collected! Jump power increased.");
        powerUpImage.enabled = false;
        text.enabled = true;

        yield return new WaitForSeconds(powerUpDuration);

        text.enabled = false;
        playerMovement.moveSpeed = originalSpeed;
        Debug.Log("Power-up effect ended. Jump power reset to original.");
    }

    
    public void ResetPowerUp()
    {

        isCollected = false;  // Reset the collection status
        Debug.Log("Super Speed Power-up Reset: " + gameObject.name);
        text.enabled = false;
        powerUpImage.enabled = true;
        Debug.Log("Super Speed Power-up reset and is now active: " + this.gameObject.activeSelf);
    }
}
