using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundConfig : MonoBehaviour
{
    SoundController soundController;

    enum SoundState { NORMAL, PERIGO, PERSEGUICAO, CAVERNA }
    SoundState state = SoundState.NORMAL;

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
        soundController.AddToBuffer("Sounds/J2/Alavanca/alavanca");
        soundController.AddToBuffer("Sounds/J2/Chao quebrando/chao quebrando");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/PJ andando");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/PJ Agachada");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/PJ correndo");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/PJ pulo");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/Idle em pe");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/Idle Agachada");
        soundController.AddToBuffer("Sounds/J2/musica medo/musica medo exploracao");
        soundController.AddToBuffer("Sounds/J2/musica medo/musica medo perigo");
        soundController.AddToBuffer("Sounds/J2/musica medo/musica medo perseguicao");
        soundController.AddToBuffer("Sounds/J2/Pedras/pedras rolando (animacao)");
        soundController.AddToBuffer("Sounds/J2/Agharta/Som de acao");
        soundController.AddToBuffer("Sounds/J2/Som Ambiente caverna/som caverna 2");
        soundController.AddToBuffer("Sounds/J2/Bondinho/bondinho");

        //Nao implementados
        soundController.AddToBuffer("Sounds/J2/Pedras Rolando-Soltando/pedra rolando");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/choro");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 1");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 2");
    }

    public void PlayAlavanca() => soundController.PlaySingleSFXByFileName("Sounds/J2/Alavanca/alavanca", true);
    public void PlayChaoQuebrando() => soundController.PlaySingleSFXByFileName("Sounds/J2/Chao quebrando/chao quebrando", true);
    public void PlayPJAndando() => soundController.PlaySFXContinuouslyByFileName("Sounds/J2/Movimentacao PJ/PJ andando", true);
    public void PlayPJAgachado() => soundController.PlaySFXContinuouslyByFileName("Sounds/J2/Movimentacao PJ/PJ Agachada", true);
    public void PlayPJCorrendo() => soundController.PlaySFXContinuouslyByFileName("Sounds/J2/Movimentacao PJ/PJ correndo", true);
    public void PlayPJPulo() => soundController.PlaySingleSFXByFileName("Sounds/J2/Movimentacao PJ/PJ pulo", true);
    public bool IsPlayingPJPulo() => soundController.IsPlayingClipByFileName("Sounds/J2/Movimentacao PJ/PJ pulo");
    public void PlayPJIdle() => soundController.PlaySFXContinuouslyByFileName("Sounds/J2/Movimentacao PJ/Idle em pe", true);
    public void PlayPJIdleAgachado() => soundController.PlaySFXContinuouslyByFileName("Sounds/J2/Movimentacao PJ/Idle Agachada", true);
    public void PlayBondinho() => soundController.PlaySFXContinuouslyByFileName("Sounds/J2/Bondinho/bondinho", true);
    public void StopSFX() => soundController.StopSFXExcept("Sounds/J2/Movimentacao PJ/PJ pulo");

    public void PlayMusicaExploracao() => soundController.PlayMusicContinuouslyByFileName("Sounds/J2/musica medo/musica medo exploracao", true);
    public void PlayMusicaPerigo() => soundController.PlayMusicContinuouslyByFileName("Sounds/J2/musica medo/musica medo perigo", true);
    public void PlayMusicaPerseguicao() => soundController.PlayMusicContinuouslyByFileName("Sounds/J2/musica medo/musica medo perseguicao", true);
    public void PlaySomCaverna() => soundController.PlayMusicContinuouslyByFileName("Sounds/J2/Som Ambiente caverna/som caverna 2", true);

    public void PlayDerrubarEstatua(AudioSource source) => soundController.PlayOnSourceByFileName(source, "Sounds/J2/Estatua/estatua quebrando", true);
    public void PlayPedrasRolando(AudioSource source) => soundController.PlayOnSourceByFileName(source, "Sounds/J2/Pedras/pedras rolando (animacao)", true);
    public void PlaySomDeAcao(AudioSource source) => soundController.PlayOnSourceByFileName(source, "Sounds/J2/Agharta/Som de acao", true);

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
