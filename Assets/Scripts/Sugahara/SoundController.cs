using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SoundController : Singleton<SoundController>
{
    AudioSource sfxSource;
    AudioSource musicSource;

    Dictionary<string, AudioClip> buffer;

    private void Awake()
    {
        CreateAudioSources();
        LoadPlayerPrefs();
        CreateBuffer();
    }

    private void CreateAudioSources()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
            musicSource.loop = true;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.loop = false;
        }
    }

    private void LoadPlayerPrefs()
    {
        sfxSource.volume = PlayerPrefs.GetInt("sfxVolume", 100);
        musicSource.volume = PlayerPrefs.GetInt("musicVolume", 100);
    }

    private void CreateBuffer()
    {
        if (buffer == null) buffer = new Dictionary<string, AudioClip>();
    }

    public void MuteMusic()
    {
        musicSource.volume = 0;
        PlayerPrefs.SetInt("musicVolume", 0);
    }

    public void UnMuteMusic()
    {
        musicSource.volume = 100;
        PlayerPrefs.SetInt("musicVolume", 100);
    }

    public void MuteSFX()
    {
        sfxSource.volume = 0;
        PlayerPrefs.SetInt("sfxVolume", 0);
    }

    public void UnMuteSFX()
    {
        sfxSource.volume = 100;
        PlayerPrefs.SetInt("sfxVolume", 100);
    }

    public void PlaySingleSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayMusicContinuously(AudioClip clip)
    {
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySingleSFXByFileName(string fileName, bool buffering)
    {
        sfxSource.clip = CreateAudioClip(fileName, buffering);
        sfxSource.Play();
    }

    public void PlayMusicByFileName(string fileName, bool buffering)
    {
        musicSource.Stop();
        musicSource.clip = CreateAudioClip(fileName, buffering);
        musicSource.Play();
    }

    public void PlayMusicContinuouslyByFileName(string fileName, bool buffering)
    {
        AudioClip clip = CreateAudioClip(fileName, buffering);

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    private AudioClip CreateAudioClip(string fileName, bool buffering)
    {
        AudioClip clip;

        if (!buffer.TryGetValue(fileName, out clip))
        {
            clip = Resources.Load<AudioClip>(fileName);

            if (buffering) buffer.Add(fileName, clip);
        }

        return clip;
    }
}
