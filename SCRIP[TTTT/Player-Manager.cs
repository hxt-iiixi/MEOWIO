using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public static bool isGameOver;
    public GameObject gameOverScreen;
    public static Vector2 lastCheckPointPos = new Vector2(0, 0);
    public GameObject[] playerPrefabs;
    public CameraFollow cameraFollow; // Reference to CameraFollow script

    private int characterIndex;
    public GameObject Player { get; private set; } // Reference to instantiated player

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Find the spawn platform
        GameObject spawnPlatform = GameObject.FindWithTag("SpawnPlatform");

        if (spawnPlatform != null)
        {
            // Set the spawn position to slightly above the platform
            lastCheckPointPos = spawnPlatform.transform.position;
            lastCheckPointPos.y += 1.0f; // Adjust as needed for your game
            Debug.Log("Spawn platform found. Spawning at: " + lastCheckPointPos);
        }
        else
        {
            Debug.LogError("Spawn platform not found. Defaulting to last checkpoint position.");
        }

        // Instantiate the selected player prefab at the spawn position
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        Player = Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);
        Player.tag = "Player";
        isGameOver = false;

        // Assign the player reference to CameraFollow if available
        if (cameraFollow != null)
        {
            cameraFollow.player = Player.transform;
            Debug.Log("CameraFollow set to follow instantiated player.");
        }
        else
        {
            Debug.LogError("CameraFollow script not assigned in PlayerManager.");
        }
    }

    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
