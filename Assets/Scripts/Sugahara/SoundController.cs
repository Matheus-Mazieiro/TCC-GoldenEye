using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SoundController : Singleton<SoundController>
{
    AudioSource sfxOneShotSource, sfxContinuousSource, musicSource;

    float lastMusicVolume, lastSFXVolume;

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

        if (sfxOneShotSource == null)
        {
            sfxOneShotSource = gameObject.AddComponent<AudioSource>();
            sfxOneShotSource.playOnAwake = false;
            sfxOneShotSource.loop = false;
        }

        if (sfxContinuousSource == null)
        {
            sfxContinuousSource = gameObject.AddComponent<AudioSource>();
            sfxContinuousSource.playOnAwake = false;
            sfxContinuousSource.loop = true;
        }
    }

    private void LoadPlayerPrefs()
    {
        lastSFXVolume = PlayerPrefs.GetInt("sfxVolume", 100);
        lastMusicVolume = PlayerPrefs.GetInt("musicVolume", 100);

        sfxOneShotSource.volume = lastSFXVolume;
        sfxContinuousSource.volume = lastSFXVolume;
        musicSource.volume = lastMusicVolume;
    }

    private void CreateBuffer()
    {
        if (buffer == null)
        {
            buffer = new Dictionary<string, AudioClip>();

            
        }
    }

    public void AddToBuffer(string fileName)
    {
        if (buffer != null && !buffer.TryGetValue(fileName, out AudioClip clip))
            buffer.Add(fileName, Resources.Load<AudioClip>(fileName));
    }

    public void ChangeMusicVolume(int volume)
    {
        musicSource.volume = volume;
        lastMusicVolume = volume;
        PlayerPrefs.SetInt("musicVolume", volume);
    }

    public void MuteMusic()
    {
        musicSource.volume = 0;
        PlayerPrefs.SetInt("musicVolume", 0);
    }

    public void UnMuteMusic()
    {
        if (lastMusicVolume == 0) lastMusicVolume = 100;

        musicSource.volume = lastMusicVolume;
        PlayerPrefs.SetInt("musicVolume", Mathf.RoundToInt(lastMusicVolume));
    }

    public void ChangeSFXVolume(int volume)
    {
        sfxOneShotSource.volume = volume;
        sfxContinuousSource.volume = volume;
        lastSFXVolume = volume;
        PlayerPrefs.SetInt("sfxVolume", volume);
    }

    public void MuteSFX()
    {
        sfxOneShotSource.volume = 0;
        sfxContinuousSource.volume = 0;
        PlayerPrefs.SetInt("sfxVolume", 0);
    }

    public void UnMuteSFX()
    {
        if (lastSFXVolume == 0) lastSFXVolume = 100;

        sfxOneShotSource.volume = lastSFXVolume;
        sfxContinuousSource.volume = lastSFXVolume;
        PlayerPrefs.SetInt("sfxVolume", Mathf.RoundToInt(lastSFXVolume));
    }

    public void PlaySingleSFX(AudioClip clip)
    {
        sfxOneShotSource.clip = clip;
        sfxOneShotSource.Play();
    }

    public void PlaySFXContinuously(AudioClip clip)
    {
        if (sfxContinuousSource.clip == clip && sfxContinuousSource.isPlaying) return;

        sfxContinuousSource.Stop();
        sfxContinuousSource.clip = clip;
        sfxContinuousSource.Play();
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
        sfxOneShotSource.clip = CreateAudioClip(fileName, buffering);
        sfxOneShotSource.Play();
    }

    public void PlaySFXContinuouslyByFileName(string fileName, bool buffering)
    {
        AudioClip clip = CreateAudioClip(fileName, buffering);

        if (sfxContinuousSource.clip == clip && sfxContinuousSource.isPlaying) return;

        sfxContinuousSource.Stop();
        sfxContinuousSource.clip = clip;
        sfxContinuousSource.Play();
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

    public void PlayOnSourceByFileName(AudioSource source, string fileName, bool buffering)
    {
        source.clip = CreateAudioClip(fileName, buffering);
        source.Play();
    }

    public void PlayOnSourceContinuouslyByFileName(AudioSource source, string fileName, bool buffering)
    {
        AudioClip clip = CreateAudioClip(fileName, buffering);

        if (source.clip == clip && source.isPlaying) return;

        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public void StopSFX()
    {
        sfxOneShotSource.Stop();
        sfxContinuousSource.Stop();
    }

    public void StopSFXExcept(string exceptionFileName)
    {
        AudioClip clip = CreateAudioClip(exceptionFileName, false);

        if (sfxOneShotSource.clip != clip) sfxOneShotSource.Stop();
        if (sfxContinuousSource.clip != clip) sfxContinuousSource.Stop();
    }

    public bool IsPlayingClipByFileName(string fileName)
    {
        AudioClip clip = CreateAudioClip(fileName, false);

        if (sfxOneShotSource.clip == clip && sfxOneShotSource.isPlaying) return true;
        else if (sfxContinuousSource.clip == clip && sfxContinuousSource.isPlaying) return true;
        else if (musicSource.clip == clip && musicSource.isPlaying) return true;

        return false;
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
