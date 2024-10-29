using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // Reference to the player's transform
    public float smoothing = 0f;  // Smoothness of the camera movement
    public Vector3 offset = new Vector3(0, 0, -100); // Adjust this as needed in Inspector

    void LateUpdate()
    {
        if (player != null)
        {
            // Follow only the player's x position, with fixed y and z
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);

            // Smoothly interpolate to the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
