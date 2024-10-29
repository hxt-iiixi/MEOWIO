using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        if (PlayerManager.Instance != null)
        {
            player = PlayerManager.Instance.Player;
        }
        else
        {
            Debug.LogError("PlayerManager instance not found.");
        }
    }

    public void MoveLeftDown()
    {
        if (player != null)
            player.GetComponent<Catscript>().PointerDownLeft();
    }

    public void MoveLeftUp()
    {
        if (player != null)
            player.GetComponent<Catscript>().PointerUpLeft();
    }

    public void MoveRightDown()
    {
        if (player != null)
            player.GetComponent<Catscript>().PointerDownRight();
    }

    public void MoveRightUp()
    {
        if (player != null)
            player.GetComponent<Catscript>().PointerUpRight();
    }

    public void JumpDown()
    {
        if (player != null)
            player.GetComponent<Catscript>().PointerDownJump();
    }

    public void JumpUp()
    {
        if (player != null)
            player.GetComponent<Catscript>().PointerUpJump();
    }
}
