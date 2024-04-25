using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource playSound;
    public void PlaySOund()
    {
        playSound.pitch = Random.Range(1.0f, 1.2f);
        playSound.Play();

    }

}
