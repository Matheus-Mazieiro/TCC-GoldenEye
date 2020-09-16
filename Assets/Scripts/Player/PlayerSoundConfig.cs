using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundConfig : MonoBehaviour
{
    SoundController soundController;

    enum SoundState { NORMAL, PERIGO, PERSEGUICAO, CAVERNA }
    SoundState state = SoundState.NORMAL;

    string acaoAghartaSoundPath = "Sounds/Agharta/Som de acao";
    string alavancaSoundPath = "Sounds/Props/alavanca";
    string bondinhoSoundPath = "Sounds/Props/medo2-sfx-bondinho";
    string chaoQuebrandoSoundPath = "Sounds/Props/chao quebrando";
    string estatuaQuebrandoSoundPath = "Sounds/Props/estatua quebrando";
    string pedrasRolando1SoundPath = "Sounds/Props/pedras rolando (animacao)";
    string pedrasRolando2SoundPath = "Sounds/Props/pedra rolando";
    string melkiAndandoSoundPath = "Sounds/Melki/PJ andando";
    string melkiAgachadaSoundPath = "Sounds/Melki/PJ Agachada";
    string melkiCorrendoSoundPath = "Sounds/Melki/PJ correndo";
    string melkiPulandoSoundPath = "Sounds/Melki/PJ pulo";
    string melkiIdleSoundPath = "Sounds/Melki/Idle em pe";
    string melkiIdleAgachadaSoundPath = "Sounds/Melki/Idle Agachada";
    string musicaExploracaoSoundPath = "Sounds/Musics/musica medo exploracao";
    string musicaPerigoSoundPath = "Sounds/Musics/musica medo exploracao";
    string musicaPerseguicaoSoundPath = "Sounds/Musics/musica medo perseguicao";
    string cavernaSoundPath = "Sounds/Background/som caverna 2";
    string goteiraSoundPath = "Sounds/Background/goteira";

    void Awake()
    {
        BufferSounds();

        SetNormal();
    }

    void LateUpdate()
    {
        switch (state)
        {
            case SoundState.NORMAL:
                PlayMusicaExploracao();
                break;
            case SoundState.PERIGO:
                PlayMusicaPerigo();
                break;
            case SoundState.PERSEGUICAO:
                PlayMusicaPerseguicao();
                break;
            case SoundState.CAVERNA:
                PlaySomCaverna();
                break;
        }
    }

    private void BufferSounds()
    {
        soundController = SoundController.Instance;

        //Implementados
        soundController.AddToBuffer(acaoAghartaSoundPath);
        soundController.AddToBuffer(alavancaSoundPath);
        soundController.AddToBuffer(bondinhoSoundPath);
        soundController.AddToBuffer(chaoQuebrandoSoundPath);
        soundController.AddToBuffer(estatuaQuebrandoSoundPath);
        soundController.AddToBuffer(pedrasRolando1SoundPath);
        soundController.AddToBuffer(pedrasRolando2SoundPath);
        soundController.AddToBuffer(melkiAndandoSoundPath);
        soundController.AddToBuffer(melkiAgachadaSoundPath);
        soundController.AddToBuffer(melkiCorrendoSoundPath);
        soundController.AddToBuffer(melkiPulandoSoundPath);
        soundController.AddToBuffer(melkiIdleSoundPath);
        soundController.AddToBuffer(melkiIdleAgachadaSoundPath);
        soundController.AddToBuffer(musicaExploracaoSoundPath);
        soundController.AddToBuffer(musicaPerigoSoundPath);
        soundController.AddToBuffer(musicaPerseguicaoSoundPath);
        soundController.AddToBuffer(cavernaSoundPath);
        soundController.AddToBuffer(goteiraSoundPath);

        //Nao implementados
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/choro");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 1");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 2");
    }

    public void PlaySomDeAcao(AudioSource source) => soundController.PlayOnSourceByFileName(source, acaoAghartaSoundPath, true);

    public void PlayAlavanca() => soundController.PlaySingleSFXByFileName(alavancaSoundPath, true);
    public void PlayBondinho() => soundController.PlaySFXContinuouslyByFileName(bondinhoSoundPath, true);
    public void PlayChaoQuebrando() => soundController.PlaySingleSFXByFileName(chaoQuebrandoSoundPath, true);
    public void PlayDerrubarEstatua(AudioSource source) => soundController.PlayOnSourceByFileName(source, estatuaQuebrandoSoundPath, true);
    public void PlayPedrasRolando(AudioSource source) => soundController.PlayOnSourceByFileName(source, pedrasRolando1SoundPath, true);
    public void PlayPJAndando() => soundController.PlaySFXContinuouslyByFileName(melkiAndandoSoundPath, true);
    public void PlayPJAgachado() => soundController.PlaySFXContinuouslyByFileName(melkiAgachadaSoundPath, true);
    public void PlayPJCorrendo() => soundController.PlaySFXContinuouslyByFileName(melkiCorrendoSoundPath, true);
    public void PlayPJPulo() => soundController.PlaySingleSFXByFileName(melkiPulandoSoundPath, true);
    public bool IsPlayingPJPulo() => soundController.IsPlayingClipByFileName(melkiPulandoSoundPath);
    public void PlayPJIdle() => soundController.PlaySFXContinuouslyByFileName(melkiIdleSoundPath, true);
    public void PlayPJIdleAgachado() => soundController.PlaySFXContinuouslyByFileName(melkiIdleAgachadaSoundPath, true);
    public void StopSFX() => soundController.StopSFXExcept(melkiPulandoSoundPath);

    public void PlayMusicaExploracao() => soundController.PlayMusicTransitionByFileName(musicaExploracaoSoundPath, true);
    public void PlayMusicaPerigo() => soundController.PlayMusicTransitionByFileName(musicaPerigoSoundPath, true);
    public void PlayMusicaPerseguicao() => soundController.PlayMusicTransitionByFileName(musicaPerseguicaoSoundPath, true);
    public void PlaySomCaverna() => soundController.PlayMusicTransitionByFileName(cavernaSoundPath, true);
    
    public void SetNormal() => state = SoundState.NORMAL;
    public void SetPerigo()
    {
        state = SoundState.PERIGO;

        if (IsInvoking()) CancelInvoke();

        Invoke("SetNormal", 3);
    }
    public void SetPerseguicao() => state = SoundState.PERSEGUICAO;
    public void SetCaverna() => state = SoundState.CAVERNA;
    public bool IsCaverna() => state == SoundState.CAVERNA;
}
