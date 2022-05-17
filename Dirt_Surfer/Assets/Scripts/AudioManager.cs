using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioMissile;
    void Update()
    {
        if(PauseMenu.gameIsPaused)
            audioMissile.pitch = 0.21f;
        else
            audioMissile.pitch = 1f;
    }
}
