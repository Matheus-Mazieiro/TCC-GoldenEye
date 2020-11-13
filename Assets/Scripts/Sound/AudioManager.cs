using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Mixer que controla o volume geral dos SFX")]
    [SerializeField] AudioMixer mixerSFX;
    AudioSource myAudioSource;
    [SerializeField] AudiosHelper[] audios;

    public Slider sfxSlider, musicSlider;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>() ?? null;

        if (sfxSlider) sfxSlider.value = SoundController.Instance.SFXVolume();
        if (musicSlider) musicSlider.value = SoundController.Instance.MusicVolume();
    }

    public void OnSFXValueChanged(float value)
    {
        mixerSFX.SetFloat("Volume", value);

        SoundController.Instance.ChangeSFXVolume((int)value);
    }

    public void OnMusicValueChanged(float value)
    {
        SoundController.Instance.ChangeMusicVolume((int)value);
    }

    public void OnBrightValueChanged(float value)
    {
        Screen.brightness = value;
    }

    /// <summary>
    /// Select a random audio from aray and play it
    /// </summary>
    /// <param name="audioKit">From wich aray should random</param>
    /// <param name="looping">Is a looping audio?</param>
    /// <param name="waitCurrentAudioEnds">Should wait current audio end?</param>
    public void PlayAudio(int audioKit, bool looping, bool waitCurrentAudioEnds)
    {
        int random = Random.Range(0, audios.Length);
        Debug.Log(audios[audioKit].audios[random]);
        //myAudioSource.clip = audios[audioKit].audios[random];
        myAudioSource.loop = looping;
        if(!waitCurrentAudioEnds)
            myAudioSource.Play();
        else if (!myAudioSource.isPlaying)
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
