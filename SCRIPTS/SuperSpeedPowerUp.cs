using System.Collections;
using UnityEngine;

public class SuperSpeedPowerUp : MonoBehaviour
{
    public float superSpeedMultiplier = 2.0f; 
    public float powerUpDuration = 5.0f;      
    public AudioClip powerUpSound;            

    private Catscript playerMovement;          
    private AudioSource audioSource;

    void Start()
    {
       
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Catscript>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
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

       
        gameObject.SetActive(false);  

      
        yield return new WaitForSeconds(powerUpDuration);

        
        playerMovement.moveSpeed = originalSpeed;
    }

    
    public void ResetPowerUp()
    {
        gameObject.SetActive(true);  
    }
}
