using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutemusic : MonoBehaviour
{
    public void Mutehandler(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;

        }
        
    }

}

 