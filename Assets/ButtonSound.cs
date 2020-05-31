using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip audioClip)
    {
        source.Stop();
        source.clip = audioClip;
        source.Play();
    }
}
