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

    void Awake()
    {
        BufferSounds();
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
    }

    public void PlayPJAndando() => soundController.PlaySFXContinuouslyByFileName(melkiAndandoSoundPath, true);
    public void PlayPJAgachado() => soundController.PlaySFXContinuouslyByFileName(melkiAgachadaSoundPath, true);
    public void PlayPJCorrendo() => soundController.PlaySFXContinuouslyByFileName(melkiCorrendoSoundPath, true);
    public void PlayPJPulo() => soundController.PlaySingleSFXByFileName(melkiPulandoSoundPath, true);
    public bool IsPlayingPJPulo() => soundController.IsPlayingClipByFileName(melkiPulandoSoundPath);
    public void PlayPJIdle() => soundController.PlaySFXContinuouslyByFileName(melkiIdleSoundPath, true);
    public void PlayPJIdleAgachado() => soundController.PlaySFXContinuouslyByFileName(melkiIdleAgachadaSoundPath, true);
    public void PlayPJMorrendo() => soundController.PlaySFXContinuouslyByFileName(melkiMorrendoSoundPath, true);
    public void StopSFX() => soundController.StopSFXExcept(melkiPulandoSoundPath);

    public void PlaySomDeAcao(AudioSource source) => soundController.PlayOnSourceByFileName(source, acaoAghartaSoundPath, true);

    public void PlayPedrasRolando(AudioSource source) => soundController.PlayOnSourceByFileName(source, pedrasRolando1SoundPath, true);
}