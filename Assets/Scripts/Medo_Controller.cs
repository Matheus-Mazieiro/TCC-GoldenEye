using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Medo_Controller : MonoBehaviour
{
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

        SoundController.Instance.PlayMusicByFileName(musicaExploracaoSoundPath, true);
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
    }

    private void BufferSounds()
    {
        //Implementados
        SoundController.Instance.AddToBuffer(musicaExploracaoSoundPath);
        SoundController.Instance.AddToBuffer(musicaPerigoSoundPath);
        SoundController.Instance.AddToBuffer(musicaPerseguicaoSoundPath);
        SoundController.Instance.AddToBuffer(cavernaSoundPath);
        SoundController.Instance.AddToBuffer(goteiraSoundPath);
        SoundController.Instance.AddToBuffer(medoAlavancaSoundPath);
        SoundController.Instance.AddToBuffer(medoBondinhoSoundPath);
        SoundController.Instance.AddToBuffer(medoChaoQuebrandoSoundPath);
        SoundController.Instance.AddToBuffer(medoEstatuaQuebrandoSoundPath);
    }

    //Medo
    public void PlayAlavancaMedo() => SoundController.Instance.PlaySingleSFXByFileName(medoAlavancaSoundPath, true);
    public void PlayBondinhoMedo() => SoundController.Instance.PlaySFXContinuouslyByFileName(medoBondinhoSoundPath, true);
    public void PlayChaoQuebrandoMedo(AudioSource source) => SoundController.Instance.PlayOnSourceByFileName(source, medoChaoQuebrandoSoundPath, true);
    public void PlayDerrubarEstatuaMedo(AudioSource source) => SoundController.Instance.PlayOnSourceByFileName(source, medoEstatuaQuebrandoSoundPath, true);
    public void PlayMusicaExploracaoMedo() => SoundController.Instance.PlayMusicTransitionByFileName(musicaExploracaoSoundPath, true);
    public void PlayMusicaPerigoMedo() => SoundController.Instance.PlayMusicTransitionByFileName(musicaPerigoSoundPath, true);
    public void PlayMusicaPerseguicaoMedo() => SoundController.Instance.PlayMusicTransitionByFileName(musicaPerseguicaoSoundPath, true);
    public void PlaySomCavernaMedo() => SoundController.Instance.PlayMusicTransitionByFileName(cavernaSoundPath, true);

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
