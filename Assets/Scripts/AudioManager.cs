﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Mixer que controla o volume geral dos SFX")]
    [SerializeField] AudioMixer mixerSFX;
    AudioSource myAudioSource;
    [SerializeField] AudiosHelper[] audios;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>() ?? null;
    }

    public void OnSFXValueChanged(float value)
    {
        mixerSFX.SetFloat("Volume", value);
    }

    /// <summary>
    /// Select a random audio from aray and play it
    /// </summary>
    /// <param name="audioKit">From wich aray should random</param>
    /// <param name="looping">is a looping audio?</param>
    public void PlayAudio(int audioKit, bool looping)
    {
        myAudioSource.clip = audios[audioKit].audios[Random.Range(0, audios.Length)];
        myAudioSource.loop = looping;
        myAudioSource.Play();
    }

    public void PlayOnlyOneTime(int audioKit)
    {
        myAudioSource.clip = audios[audioKit].audios[Random.Range(0, audios.Length)];
        myAudioSource.loop = false;
        myAudioSource.Play();
    }

    public void StopAudio()
    {
        myAudioSource.Pause();
    }
}

[System.Serializable]
struct AudiosHelper
{
    public AudioClip[] audios;
}
