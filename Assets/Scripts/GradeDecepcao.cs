using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Audio;

public class GradeDecepcao : MonoBehaviour
{
    SoundController sound;

    AudioMixer mixer;

    AudioSource sfxOneShotSource;

    string totem;

    public Transform target;

    public EnemyBehaviour enemy1, enemy2;

    private void Awake()
    {
        sound = SoundController.Instance;

        totem = "Sounds/Props/decepcao2-sfx-porta-madeira";

        sound.AddToBuffer(totem);

        mixer = Resources.Load<AudioMixer>("Sounds/StandardMixer");

        AudioMixerGroup sfxGroup = mixer.FindMatchingGroups("SFX").FirstOrDefault();

        if (sfxOneShotSource == null)
        {
            sfxOneShotSource = gameObject.AddComponent<AudioSource>();
            sfxOneShotSource.playOnAwake = false;
            sfxOneShotSource.loop = false;
            sfxOneShotSource.outputAudioMixerGroup = sfxGroup;
        }
    }

    public void InvokeGrade(float time)
    {
        Invoke("PlaySound", time);
    }

    void PlaySound()
    {
        sound.PlayOnSourceByFileName(sfxOneShotSource, totem, true);

        Invoke("GoCheck1", 0.4f);
        Invoke("GoCheck2", 0.6f);
    }

    void GoCheck1() => enemy1.GoCheck(target);
    void GoCheck2() => enemy2.GoCheck(target);
}
