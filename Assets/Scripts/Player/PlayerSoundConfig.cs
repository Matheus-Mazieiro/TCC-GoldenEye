using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundConfig : MonoBehaviour
{
    SoundController soundController;

    enum SoundState { MEDO_EXPLORACAO, MEDO_PERIGO, MEDO_PERSEGUICAO, MEDO_CAVERNA, SOLIDAO_1, SOLIDAO_3 }
    SoundState state = SoundState.MEDO_EXPLORACAO;

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

    //Medo
    string musicaExploracaoSoundPath = "Sounds/Musics/musica medo exploracao";
    string musicaPerigoSoundPath = "Sounds/Musics/musica medo exploracao";
    string musicaPerseguicaoSoundPath = "Sounds/Musics/musica medo perseguicao";
    string cavernaSoundPath = "Sounds/Background/som caverna 2";
    string goteiraSoundPath = "Sounds/Background/goteira";
    string medoAlavancaSoundPath = "Sounds/Props/alavanca";
    string medoBondinhoSoundPath = "Sounds/Props/medo2-sfx-bondinho";
    string medoChaoQuebrandoSoundPath = "Sounds/Props/chao quebrando";
    string medoEstatuaQuebrandoSoundPath = "Sounds/Props/estatua quebrando";

    //Solidão
    string musicaSolidaoAmbiente1SoundPath = "Sounds/Musics/solidao1-sfx-amb-externo";
    string musicaSolidaoAmbiente3SoundPath = "Sounds/Musics/solidao3-sfx-amb-externo";
    string solidaoMoinhoSoundPath = "Sounds/Props/solidao1-sfx-moinho";
    string solidaoAlavancaSoundPath = "Sounds/Props/solidao2-sfx-alavanca";
    string solidaoChaoQuebrandoSoundPath = "Sounds/Props/solidao2-sfx-chao-quebrando";
    string solidaoPortaSoundPath = "Sounds/Props/solidao2-sfx-ptrancada1";
    string solidaoRoldanaSoundPath = "Sounds/Props/solidao2-sfx-roldana";
    string solidaoCarrinhoSoundPath = "Sounds/Props/solidao4-sfx-carrinho";
    string solidaoEngrenagem1SoundPath = "Sounds/Props/solidao-sfx-engrenagem1";
    string solidaoEngrenagem2SoundPath = "Sounds/Props/solidao-sfx-engrenagem2";

    void Awake()
    {
        BufferSounds();

        SetMedoExploracao();
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
            case SoundState.SOLIDAO_1:
                PlayMusicaAmbiente1Solidao();
                break;
            case SoundState.SOLIDAO_3:
                PlayMusicaAmbiente3Solidao();
                break;
        }
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

    //Medo
    public void PlayAlavancaMedo() => soundController.PlaySingleSFXByFileName(medoAlavancaSoundPath, true);
    public void PlayBondinhoMedo() => soundController.PlaySFXContinuouslyByFileName(medoBondinhoSoundPath, true);
    public void PlayChaoQuebrandoMedo() => soundController.PlaySingleSFXByFileName(medoChaoQuebrandoSoundPath, true);
    public void PlayDerrubarEstatuaMedo(AudioSource source) => soundController.PlayOnSourceByFileName(source, medoEstatuaQuebrandoSoundPath, true);
    public void PlayMusicaExploracaoMedo() => soundController.PlayMusicTransitionByFileName(musicaExploracaoSoundPath, true);
    public void PlayMusicaPerigoMedo() => soundController.PlayMusicTransitionByFileName(musicaPerigoSoundPath, true);
    public void PlayMusicaPerseguicaoMedo() => soundController.PlayMusicTransitionByFileName(musicaPerseguicaoSoundPath, true);
    public void PlaySomCavernaMedo() => soundController.PlayMusicTransitionByFileName(cavernaSoundPath, true);

    //Solidão
    //public void PlayMoinhoSolidao(AudioSource source) => soundController.PlayOnSourceByFileName(source, solidaoMoinhoSoundPath, true);
    public void PlayMoinhoSolidao() => soundController.PlaySingleSFXByFileName(solidaoMoinhoSoundPath, true);
    public void PlayAlavancaSolidao() => soundController.PlaySingleSFXByFileName(solidaoAlavancaSoundPath, true);
    public void PlayCarrinhoSolidao() => soundController.PlaySFXContinuouslyByFileName(solidaoCarrinhoSoundPath, true);
    public void PlayPortaSolidao() => soundController.PlaySFXContinuouslyByFileName(solidaoPortaSoundPath, true);
    public void PlayRoldanaSolidao() => soundController.PlaySFXContinuouslyByFileName(solidaoRoldanaSoundPath, true);
    public void PlayChaoQuebrandoSolidao() => soundController.PlaySingleSFXByFileName(solidaoChaoQuebrandoSoundPath, true);
    public void PlayEngrenagem1Solidao() => soundController.PlaySFXContinuouslyByFileName(solidaoEngrenagem1SoundPath, true);
    public void PlayEngrenagem2Solidao() => soundController.PlaySFXContinuouslyByFileName(solidaoEngrenagem2SoundPath, true);
    public void PlayMusicaAmbiente1Solidao() => soundController.PlayMusicTransitionByFileName(musicaSolidaoAmbiente1SoundPath, true);
    public void PlayMusicaAmbiente3Solidao() => soundController.PlayMusicTransitionByFileName(musicaSolidaoAmbiente3SoundPath, true);

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

    public void SetSolidao1() => state = SoundState.SOLIDAO_1;
    public void SetSolidao3() => state = SoundState.SOLIDAO_3;
}
