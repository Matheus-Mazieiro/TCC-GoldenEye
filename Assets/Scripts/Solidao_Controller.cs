﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Solidao_Controller : MonoBehaviour
{
    SoundController soundController;

    enum SoundState { SOLIDAO_1, SOLIDAO_3 }
    SoundState state = SoundState.SOLIDAO_1;

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

    void Start()
    {
        BufferSounds();

        soundController.PlayMusicContinuouslyByFileName(musicaSolidaoAmbiente1SoundPath, true);
    }

    void LateUpdate()
    {
        switch (state)
        {
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
        soundController.AddToBuffer(musicaSolidaoAmbiente1SoundPath);
        soundController.AddToBuffer(musicaSolidaoAmbiente3SoundPath);
        soundController.AddToBuffer(solidaoMoinhoSoundPath);
        soundController.AddToBuffer(solidaoAlavancaSoundPath);
        soundController.AddToBuffer(solidaoChaoQuebrandoSoundPath);
        soundController.AddToBuffer(solidaoPortaSoundPath);
        soundController.AddToBuffer(solidaoRoldanaSoundPath);
        soundController.AddToBuffer(solidaoCarrinhoSoundPath);
        soundController.AddToBuffer(solidaoEngrenagem1SoundPath);
        soundController.AddToBuffer(solidaoEngrenagem2SoundPath);
    }

    //Solidão
    //public void PlayMoinhoSolidao(AudioSource source) => soundController.PlayOnSourceByFileName(source, solidaoMoinhoSoundPath, true);
    //public void PlayMoinhoSolidao() => soundController.PlaySFXContinuouslyByFileName(solidaoMoinhoSoundPath, true);
    public void PlayMoinhoSolidao(AudioSource source) => soundController.PlayOnSourceByFileName(source, solidaoMoinhoSoundPath, true);
    public void PlayAlavancaSolidao(AudioSource source) => soundController.PlayOnSourceByFileName(source, solidaoAlavancaSoundPath, true);
    public void PlayCarrinhoSolidao(AudioSource source) => soundController.PlayOnSourceContinuouslyByFileName(source, solidaoCarrinhoSoundPath, true);
    public void PlayPortaSolidao(AudioSource source) => soundController.PlayOnSourceByFileName(source, solidaoPortaSoundPath, true);
    public void PlayRoldanaSolidao(AudioSource source) => soundController.PlayOnSourceByFileName(source, solidaoRoldanaSoundPath, true);
    public void PlayChaoQuebrandoSolidao(AudioSource source) => soundController.PlayOnSourceByFileName(source, solidaoChaoQuebrandoSoundPath, true);
    public void PlayEngrenagem1Solidao(AudioSource source) => soundController.PlayOnSourceContinuouslyByFileName(source, solidaoEngrenagem1SoundPath, true);
    public void PlayEngrenagem2Solidao(AudioSource source) => soundController.PlayOnSourceContinuouslyByFileName(source, solidaoEngrenagem2SoundPath, true);
    public void PlayMusicaAmbiente1Solidao() => soundController.PlayMusicTransitionByFileName(musicaSolidaoAmbiente1SoundPath, true);
    public void PlayMusicaAmbiente3Solidao() => soundController.PlayMusicTransitionByFileName(musicaSolidaoAmbiente3SoundPath, true);

    public void SetSolidao1() => state = SoundState.SOLIDAO_1;
    public void SetSolidao3() => state = SoundState.SOLIDAO_3;
}
