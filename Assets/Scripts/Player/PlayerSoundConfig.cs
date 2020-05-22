﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundConfig : MonoBehaviour
{
    SoundController soundController;

    void Awake()
    {
        BufferSounds();

        PlayMusicaExploracao();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BufferSounds()
    {
        soundController = SoundController.Instance;

        //Implementados
        soundController.AddToBuffer("Sounds/J2/Alavanca/alavanca");
        soundController.AddToBuffer("Sounds/J2/Chao quebrando/chao quebrando");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/PJ andando");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/PJ agachado");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/PJ correndo");
        soundController.AddToBuffer("Sounds/J2/Movimentacao PJ/PJ pulo");
        soundController.AddToBuffer("Sounds/J2/música medo/musica medo exploração");
        soundController.AddToBuffer("Sounds/J2/música medo/musica medo perigo");
        soundController.AddToBuffer("Sounds/J2/música medo/musica medo perseguição");
        soundController.AddToBuffer("Sounds/J2/Pedras/pedras rolando (animaçao)");

        //Nao implementados
        soundController.AddToBuffer("Sounds/J2/Agharta/Som de acao");
        soundController.AddToBuffer("Sounds/J2/Som Ambiente caverna/som caverna 1");
        soundController.AddToBuffer("Sounds/J2/Som Ambiente caverna/som caverna 2");
        soundController.AddToBuffer("Sounds/J2/Pedras Rolando-Soltando/pedra rolando");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/choro");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 1");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 2");
    }

    public void PlayAlavanca() => soundController.PlaySingleSFXByFileName("Sounds/J2/Alavanca/alavanca", true);
    public void PlayChaoQuebrando() => soundController.PlaySingleSFXByFileName("Sounds/J2/Chao quebrando/chao quebrando", true);
    public void PlayPJAndando() => soundController.PlaySFXContinuouslyByFileName("Sounds/J2/Movimentacao PJ/PJ andando", true);
    public void PlayPJAgachado() => soundController.PlaySFXContinuouslyByFileName("Sounds/J2/Movimentacao PJ/PJ agachado", true);
    public void PlayPJCorrendo() => soundController.PlaySFXContinuouslyByFileName("Sounds/J2/Movimentacao PJ/PJ correndo", true);
    public void PlayPJPulo() => soundController.PlaySingleSFXByFileName("Sounds/J2/Movimentacao PJ/PJ pulo", true);
    public bool IsPlayingPJPulo() => soundController.IsPlayingClipByFileName("Sounds/J2/Movimentacao PJ/PJ pulo");
    public void StopSFX() => soundController.StopSFXExcept("Sounds/J2/Movimentacao PJ/PJ pulo");

    public void PlayMusicaExploracao() => soundController.PlayMusicContinuouslyByFileName("Sounds/J2/música medo/musica medo exploração", true);
    public void PlayMusicaPerigo() => soundController.PlayMusicContinuouslyByFileName("Sounds/J2/música medo/musica medo perigo", true);
    public void PlayMusicaPerseguicao() => soundController.PlayMusicContinuouslyByFileName("Sounds/J2/música medo/musica medo perseguição", true);

    public void PlayDerrubarEstatua(AudioSource source) => soundController.PlayOnSourceByFileName(source, "Sounds/J2/Estatua/estatua quebrando", true);
    public void PlayPedrasRolando(AudioSource source) => soundController.PlayOnSourceByFileName(source, "Sounds/J2/Pedras/pedras rolando (animaçao)", true);
    public void PlaySomDeAcao(AudioSource source) => soundController.PlayOnSourceByFileName(source, "Sounds/J2/Agharta/Som de acao", true);
}
