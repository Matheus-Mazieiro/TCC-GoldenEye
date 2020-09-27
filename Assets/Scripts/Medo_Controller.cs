using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Medo_Controller : MonoBehaviour
{
    SoundController soundController;

    enum SoundState { MEDO_EXPLORACAO, MEDO_PERIGO, MEDO_PERSEGUICAO, MEDO_CAVERNA }
    SoundState state = SoundState.MEDO_EXPLORACAO;

    //Medo
    string musicaExploracaoSoundPath = "Sounds/Musics/musica medo exploracao";
    string musicaPerigoSoundPath = "Sounds/Musics/musica medo perigo";
    string musicaPerseguicaoSoundPath = "Sounds/Musics/musica medo perseguicao";
    string cavernaSoundPath = "Sounds/Background/som caverna 2";
    string goteiraSoundPath = "Sounds/Background/goteira";
    string medoAlavancaSoundPath = "Sounds/Props/alavanca";
    string medoBondinhoSoundPath = "Sounds/Props/medo2-sfx-bondinho";
    string medoChaoQuebrandoSoundPath = "Sounds/Props/chao quebrando";
    string medoEstatuaQuebrandoSoundPath = "Sounds/Props/estatua quebrando";

    void Start()
    {
        BufferSounds();

        soundController.PlayMusicContinuouslyByFileName(musicaExploracaoSoundPath, true);
    }

    void LateUpdate()
    {
        switch (state)
        {
            case SoundState.MEDO_EXPLORACAO:
                PlayMusicaExploracaoMedo();
                break;
            case SoundState.MEDO_PERIGO:
                PlayMusicaPerigoMedo();
                break;
            case SoundState.MEDO_PERSEGUICAO:
                PlayMusicaPerseguicaoMedo();
                break;
            case SoundState.MEDO_CAVERNA:
                PlaySomCavernaMedo();
                break;
        }

        if (!IsMedoCaverna()) SetMedoExploracao();
    }

    private void BufferSounds()
    {
        soundController = SoundController.Instance;

        //Implementados
        soundController.AddToBuffer(musicaExploracaoSoundPath);
        soundController.AddToBuffer(musicaPerigoSoundPath);
        soundController.AddToBuffer(musicaPerseguicaoSoundPath);
        soundController.AddToBuffer(cavernaSoundPath);
        soundController.AddToBuffer(goteiraSoundPath);
        soundController.AddToBuffer(medoAlavancaSoundPath);
        soundController.AddToBuffer(medoBondinhoSoundPath);
        soundController.AddToBuffer(medoChaoQuebrandoSoundPath);
        soundController.AddToBuffer(medoEstatuaQuebrandoSoundPath);
    }

    //Medo
    public void PlayAlavancaMedo() => soundController.PlaySingleSFXByFileName(medoAlavancaSoundPath, true);
    public void PlayBondinhoMedo() => soundController.PlaySFXContinuouslyByFileName(medoBondinhoSoundPath, true);
    public void PlayChaoQuebrandoMedo() => soundController.PlaySingleSFXByFileName(medoChaoQuebrandoSoundPath, true);
    public void PlayDerrubarEstatuaMedo(AudioSource source) => soundController.PlayOnSourceByFileName(source, medoEstatuaQuebrandoSoundPath, true);
    public void PlayMusicaExploracaoMedo() => soundController.PlayMusicTransitionByFileName(musicaExploracaoSoundPath, true);
    public void PlayMusicaPerigoMedo() => soundController.PlayMusicTransitionByFileName(musicaPerigoSoundPath, true);
    public void PlayMusicaPerseguicaoMedo() => soundController.PlayMusicTransitionByFileName(musicaPerseguicaoSoundPath, true);
    public void PlaySomCavernaMedo() => soundController.PlayMusicTransitionByFileName(cavernaSoundPath, true);

    public void SetMedoExploracao() => state = SoundState.MEDO_EXPLORACAO;
    public void SetMedoPerigo()
    {
        state = SoundState.MEDO_PERIGO;

        if (IsInvoking()) CancelInvoke();

        Invoke("SetMedoExploracao", 3);
    }
    public void SetMedoPerseguicao() => state = SoundState.MEDO_PERSEGUICAO;
    public void SetMedoCaverna() => state = SoundState.MEDO_CAVERNA;
    public bool IsMedoCaverna() => state == SoundState.MEDO_CAVERNA;
}
