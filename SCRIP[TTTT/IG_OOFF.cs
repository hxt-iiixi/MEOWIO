using UnityEngine;
using UnityEngine.UI; // For Button
using TMPro;

public class IG_OOFF : MonoBehaviour
{
    // Reference to the on/off buttons in the hierarchy
    public Button onButton;
    public Button offButton;

    private bool isOn; // para malaman niya kung on/off yung sound (On/Off)

    private void Start()
    {
        // Load the saved toggle state from PlayerPrefs, default to "on" (0)
        isOn = PlayerPrefs.GetInt("Muted", 0) == 0; // Load sound state (0 = on, 1 = off)

        // para masave at di mabago
        UpdateButtonVisibility();
    }

    public void ToggleSoundState()
    {

        if (sound.Instance != null)
        {
            sound.Instance.ToggleSound(); // Toggle the sound on/off
        }

        // Flip the isOn state
        isOn = !isOn;

        // Save niya yung bagong toggle na button (0 for On, 1 for Off)
        PlayerPrefs.SetInt("Muted", isOn ? 0 : 1);
        PlayerPrefs.Save(); // Save sound

        UpdateButtonVisibility();// para masave at di mabago
    }

    public void UpdateButtonVisibility()
    {
        onButton.gameObject.SetActive(isOn);  // Show onButton if isOn is true
        offButton.gameObject.SetActive(!isOn); // Show offButton if isOn is false
    }

}
