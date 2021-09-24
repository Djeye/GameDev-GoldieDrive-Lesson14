using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSounds : MonoBehaviour
{
    private bool mute = false;

    public void TriggerMute()
    {
        mute = !mute;

        if (mute)
            AudioListener.volume = 0f;

        else
            AudioListener.volume = 1f;

    }

}
