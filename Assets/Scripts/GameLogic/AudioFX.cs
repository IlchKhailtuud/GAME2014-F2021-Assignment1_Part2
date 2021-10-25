using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickFX;

    public void PlayClickFX()
    {
        audioSource.PlayOneShot(clickFX);
    }
}
