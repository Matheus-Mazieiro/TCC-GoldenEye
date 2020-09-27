using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Audio;
using System.Collections;

public class SoundController : Singleton<SoundController>
{
    AudioMixer mixer;

    AudioSource sfxOneShotSource, sfxContinuousSource;

    AudioSource[] musicSource;

    float lastMusicVolume, lastSFXVolume;

    int activeMusicSource = 0;

    Dictionary<string, AudioClip> buffer;

    private void Awake()
    {
        CreateAudioSources();
        LoadPlayerPrefs();
        CreateBuffer();
    }

    private void CreateAudioSources()
    {
        mixer = Resources.Load<AudioMixer>("Sounds/StandardMixer");

        //AudioMixerGroup masterGroup = mixer.FindMatchingGroups("Master").FirstOrDefault();
        AudioMixerGroup musicGroup = mixer.FindMatchingGroups("Music").FirstOrDefault();
        AudioMixerGroup sfxGroup = mixer.FindMatchingGroups("SFX").FirstOrDefault();

        if (musicSource == null) musicSource = new AudioSource[2];

        if (musicSource[0] == null)
        {
            musicSource[0] = gameObject.AddComponent<AudioSource>();
            musicSource[0].playOnAwake = false;
            musicSource[0].loop = true;
            musicSource[0].outputAudioMixerGroup = musicGroup;
        }

        if (musicSource[1] == null)
        {
            musicSource[1] = gameObject.AddComponent<AudioSource>();
            musicSource[1].playOnAwake = false;
            musicSource[1].loop = true;
            musicSource[1].outputAudioMixerGroup = musicGroup;
        }

        if (sfxOneShotSource == null)
        {
            sfxOneShotSource = gameObject.AddComponent<AudioSource>();
            sfxOneShotSource.playOnAwake = false;
            sfxOneShotSource.loop = false;
            sfxOneShotSource.outputAudioMixerGroup = sfxGroup;
        }

        if (sfxContinuousSource == null)
        {
            sfxContinuousSource = gameObject.AddComponent<AudioSource>();
            sfxContinuousSource.playOnAwake = false;
            sfxContinuousSource.loop = true;
            sfxContinuousSource.outputAudioMixerGroup = sfxGroup;
        }
    }

    private void LoadPlayerPrefs()
    {
        lastSFXVolume = PlayerPrefs.GetInt("sfxVolume", 0);
        lastMusicVolume = PlayerPrefs.GetInt("musicVolume", 0);

        mixer.SetFloat("SFXVolume", lastSFXVolume);
        mixer.SetFloat("MusicVolume", lastMusicVolume);
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

    public void MuteAll()
    {
        mixer.SetFloat("MasterVolume", -80);
    }

    public void LowerAll()
    {
        mixer.SetFloat("MasterVolume", -20);
    }

    public void UnMuteAll()
    {
        mixer.SetFloat("MasterVolume", 0);
    }

    public void ChangeMusicVolume(int volume)
    {
        volume = Mathf.Clamp(volume, -80, 0);
        mixer.SetFloat("MusicVolume", volume);
        lastMusicVolume = volume;
        PlayerPrefs.SetInt("musicVolume", volume);
    }

    public void MuteMusic()
    {
        mixer.SetFloat("MusicVolume", -80);
        PlayerPrefs.SetInt("musicVolume", -80);
    }

    public void UnMuteMusic()
    {
        if (lastMusicVolume == -80) lastMusicVolume = 0;

        mixer.SetFloat("MusicVolume", lastMusicVolume);
        PlayerPrefs.SetInt("musicVolume", Mathf.RoundToInt(lastMusicVolume));
    }

    public void ChangeSFXVolume(int volume)
    {
        volume = Mathf.Clamp(volume, -80, 0);
        mixer.SetFloat("SFXVolume", volume);
        lastSFXVolume = volume;
        PlayerPrefs.SetInt("sfxVolume", volume);
    }

    public void MuteSFX()
    {
        mixer.SetFloat("SFXVolume", -80);
        PlayerPrefs.SetInt("sfxVolume", -80);
    }

    public void UnMuteSFX()
    {
        if (lastSFXVolume == -80) lastSFXVolume = 0;

        mixer.SetFloat("SFXVolume", lastSFXVolume);
        PlayerPrefs.SetInt("sfxVolume", Mathf.RoundToInt(lastSFXVolume));
    }

    public void PlaySingleSFX(AudioClip clip)
    {
        sfxOneShotSource.Stop();
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
        musicSource[activeMusicSource].Stop();
        musicSource[activeMusicSource].clip = clip;
        musicSource[activeMusicSource].Play();
    }

    public void PlayMusicContinuously(AudioClip clip)
    {
        if (musicSource[activeMusicSource].clip == clip && musicSource[activeMusicSource].isPlaying) return;

        musicSource[activeMusicSource].Stop();
        musicSource[activeMusicSource].clip = clip;
        musicSource[activeMusicSource].Play();
    }

    public void PlayMusicTransition(AudioClip clip)
    {
        if (musicSource[activeMusicSource].clip == clip && musicSource[activeMusicSource].isPlaying) return;

        StartCoroutine(Transition(20, clip));
    }

    public void PlaySingleSFXByFileName(string fileName, bool buffering)
    {
        sfxOneShotSource.Stop();
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
        musicSource[activeMusicSource].Stop();
        musicSource[activeMusicSource].clip = CreateAudioClip(fileName, buffering);
        musicSource[activeMusicSource].Play();
    }

    public void PlayMusicContinuouslyByFileName(string fileName, bool buffering)
    {
        AudioClip clip = CreateAudioClip(fileName, buffering);

        if (musicSource[activeMusicSource].clip == clip && musicSource[activeMusicSource].isPlaying) return;

        musicSource[activeMusicSource].Stop();
        musicSource[activeMusicSource].clip = clip;
        musicSource[activeMusicSource].Play();
    }

    public void PlayMusicTransitionByFileName(string fileName, bool buffering)
    {
        AudioClip clip = CreateAudioClip(fileName, buffering);

        if (musicSource[activeMusicSource].clip == clip && musicSource[activeMusicSource].isPlaying) return;

        StartCoroutine(Transition(20, clip));
    }

    public void PlayOnSourceByFileName(AudioSource source, string fileName, bool buffering)
    {
        source.Stop();
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
        else if (musicSource[0].clip == clip && musicSource[0].isPlaying) return true;
        else if (musicSource[1].clip == clip && musicSource[1].isPlaying) return true;

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

    IEnumerator Transition(int duration, AudioClip clip)
    {
        int nextMusicSource = activeMusicSource == 0 ? 1 : 0;

        musicSource[nextMusicSource].Stop();
        musicSource[nextMusicSource].clip = clip;
        musicSource[nextMusicSource].volume = 0;
        musicSource[nextMusicSource].Play();

        for (int i = 0; i <= duration; i++)
        {
            musicSource[activeMusicSource].volume -= 1f / (float)duration;
            musicSource[nextMusicSource].volume += 1f / (float)duration;
            yield return new WaitForSeconds(0.1f);
        }

        activeMusicSource = nextMusicSource;
    }
}
