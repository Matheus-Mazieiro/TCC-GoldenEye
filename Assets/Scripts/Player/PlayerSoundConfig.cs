using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerSoundConfig : MonoBehaviour
{
    SoundController soundController;

    string acaoAghartaSoundPath = "Sounds/Agharta/Som de acao";
    string pedrasRolando1SoundPath = "Sounds/Props/pedras rolando (animacao)";
    string pedrasRolando2SoundPath = "Sounds/Props/pedra rolando";
    string melkiAndandoSoundPath = "Sounds/Melki/PJ andando";
    string melkiAgachadaSoundPath = "Sounds/Melki/PJ Agachada";
    string melkiCorrendoSoundPath = "Sounds/Melki/PJ correndo";
    string melkiPulandoSoundPath = "Sounds/Melki/PJ pulo";
    string melkiIdleSoundPath = "Sounds/Melki/Idle em pe";
    string melkiIdleAgachadaSoundPath = "Sounds/Melki/Idle Agachada";
    string melkiMorrendoSoundPath = "Sounds/Melki/Melki-sfx-som-de-morte";

    List<string> walks = new List<string>();
    List<string> walks_ext = new List<string>();
    List<string> runs = new List<string>();
    List<string> runs_ext = new List<string>();

    Coroutine walking, walking_ext, running, running_ext;

    public float walkStep = 0, runStep = 0;

    void Awake()
    {
        BufferSounds();

        if (walkStep <= 0) walkStep = 0.95f;
        if (runStep <= 0) runStep = 0.45f;
    }

    private void BufferSounds()
    {
        soundController = SoundController.Instance;

        //Implementados
        soundController.AddToBuffer(acaoAghartaSoundPath);
        soundController.AddToBuffer(pedrasRolando1SoundPath);
        soundController.AddToBuffer(pedrasRolando2SoundPath);
        soundController.AddToBuffer(melkiAndandoSoundPath);
        soundController.AddToBuffer(melkiAgachadaSoundPath);
        soundController.AddToBuffer(melkiCorrendoSoundPath);
        soundController.AddToBuffer(melkiPulandoSoundPath);
        soundController.AddToBuffer(melkiIdleSoundPath);
        soundController.AddToBuffer(melkiIdleAgachadaSoundPath);
        soundController.AddToBuffer(melkiMorrendoSoundPath);

        walks.Add("Sounds/Melki/Passos/melki-sfx-andando1");
        soundController.AddToBuffer(walks[0]);
        walks.Add("Sounds/Melki/Passos/melki-sfx-andando2");
        soundController.AddToBuffer(walks[1]);
        walks.Add("Sounds/Melki/Passos/melki-sfx-andando3");
        soundController.AddToBuffer(walks[2]);
        walks.Add("Sounds/Melki/Passos/melki-sfx-andando4");
        soundController.AddToBuffer(walks[3]);
        walks.Add("Sounds/Melki/Passos/melki-sfx-andando5");
        soundController.AddToBuffer(walks[4]);

        walks_ext.Add("Sounds/Melki/Passos/melki-sfx-andando-ext1");
        soundController.AddToBuffer(walks_ext[0]);
        walks_ext.Add("Sounds/Melki/Passos/melki-sfx-andando-ext2");
        soundController.AddToBuffer(walks_ext[1]);
        walks_ext.Add("Sounds/Melki/Passos/melki-sfx-andando-ext3");
        soundController.AddToBuffer(walks_ext[2]);
        walks_ext.Add("Sounds/Melki/Passos/melki-sfx-andando-ext4");
        soundController.AddToBuffer(walks_ext[3]);
        walks_ext.Add("Sounds/Melki/Passos/melki-sfx-andando-ext5");
        soundController.AddToBuffer(walks_ext[4]);

        runs.Add("Sounds/Melki/Passos/melki-sfx-correndo1");
        soundController.AddToBuffer(runs[0]);
        runs.Add("Sounds/Melki/Passos/melki-sfx-correndo2");
        soundController.AddToBuffer(runs[1]);
        runs.Add("Sounds/Melki/Passos/melki-sfx-correndo3");
        soundController.AddToBuffer(runs[2]);
        runs.Add("Sounds/Melki/Passos/melki-sfx-correndo4");
        soundController.AddToBuffer(runs[3]);
        runs.Add("Sounds/Melki/Passos/melki-sfx-correndo5");
        soundController.AddToBuffer(runs[4]);

        runs_ext.Add("Sounds/Melki/Passos/melki-sfx-correndo-ext1");
        soundController.AddToBuffer(runs_ext[0]);
        runs_ext.Add("Sounds/Melki/Passos/melki-sfx-correndo-ext2");
        soundController.AddToBuffer(runs_ext[1]);
        runs_ext.Add("Sounds/Melki/Passos/melki-sfx-correndo-ext3");
        soundController.AddToBuffer(runs_ext[2]);
        runs_ext.Add("Sounds/Melki/Passos/melki-sfx-correndo-ext4");
        soundController.AddToBuffer(runs_ext[3]);
        runs_ext.Add("Sounds/Melki/Passos/melki-sfx-correndo-ext5");
        soundController.AddToBuffer(runs_ext[4]);
    }

    //public void PlayPJAndando() => soundController.PlaySFXContinuouslyByFileName(melkiAndandoSoundPath, true);
    //public void PlayPJCorrendo() => soundController.PlaySFXContinuouslyByFileName(melkiCorrendoSoundPath, true);
    public void PlayPJAndando()
    {
        if (walking != null) return;

        StopStepping();

        walking = StartCoroutine(Walking());
    }

    public void PlayPJAndando_ext()
    {
        if (walking_ext != null) return;

        StopStepping();

        walking_ext = StartCoroutine(Walking_ext());
    }

    public void PlayPJCorrendo()
    {
        if (running != null) return;

        StopStepping();

        running = StartCoroutine(Running());
    }

    public void PlayPJCorrendo_ext()
    {
        if (running_ext != null) return;

        StopStepping();

        running_ext = StartCoroutine(Running_ext());
    }

    IEnumerator Walking()
    {
        while (true)
        {
            int index = Random.Range(0, walks.Count);
            soundController.PlayStepsByFileName(walks[index], true);
            yield return new WaitForSeconds(walkStep);
        }
    }

    IEnumerator Walking_ext()
    {
        while (true)
        {
            int index = Random.Range(0, walks_ext.Count);
            soundController.PlayStepsByFileName(walks_ext[index], true);
            yield return new WaitForSeconds(walkStep);
        }
    }

    IEnumerator Running()
    {
        while (true)
        {
            int index = Random.Range(0, runs.Count);
            soundController.PlayStepsByFileName(runs[index], true);
            yield return new WaitForSeconds(runStep);
        }
    }

    IEnumerator Running_ext()
    {
        while (true)
        {
            int index = Random.Range(0, runs_ext.Count);
            soundController.PlayStepsByFileName(runs_ext[index], true);
            yield return new WaitForSeconds(runStep);
        }
    }

    void StopStepping()
    {
        if (walking != null)
        {
            StopCoroutine(walking);
            walking = null;
        }
        if (walking_ext != null)
        {
            StopCoroutine(walking_ext);
            walking_ext = null;
        }
        if (running != null)
        {
            StopCoroutine(running);
            running = null;
        }
        if (running_ext != null)
        {
            StopCoroutine(running_ext);
            running_ext = null;
        }

        soundController.StopSteps();
    }

    public void PlayPJAgachado() => soundController.PlaySFXContinuouslyByFileName(melkiAgachadaSoundPath, true);
    public void PlayPJPulo() => soundController.PlaySingleSFXByFileName(melkiPulandoSoundPath, true);
    public bool IsPlayingPJPulo() => soundController.IsPlayingClipByFileName(melkiPulandoSoundPath);
    public void PlayPJIdle() => soundController.PlaySFXContinuouslyByFileName(melkiIdleSoundPath, true);
    public void PlayPJIdleAgachado() => soundController.PlaySFXContinuouslyByFileName(melkiIdleAgachadaSoundPath, true);
    public void PlayPJMorrendo() => soundController.PlaySFXContinuouslyByFileName(melkiMorrendoSoundPath, true);
    public void StopSFX()
    {
        List<string> exceptions = new List<string>();

        exceptions.Add(melkiPulandoSoundPath);
        exceptions.Add(melkiIdleSoundPath);

        soundController.StopSFXExcept(exceptions);
    }
    public void StopPJIdle() => soundController.StopSFXByFileName(melkiIdleSoundPath);

    public void PlaySomDeAcao(AudioSource source) => soundController.PlayOnSourceByFileName(source, acaoAghartaSoundPath, true);

    public void PlayPedrasRolando(AudioSource source) => soundController.PlayOnSourceByFileName(source, pedrasRolando1SoundPath, true);
}