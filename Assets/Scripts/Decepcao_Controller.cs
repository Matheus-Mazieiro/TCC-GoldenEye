using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decepcao_Controller : MonoBehaviour
{
    SoundController soundController;

    //enum SoundState { SOLIDAO_1, SOLIDAO_3 }
    //SoundState state = SoundState.SOLIDAO_1;

    //Decepção
    string musicaSoundPath = "Sounds/Musics/decepcao-musica-decepcao";

    string sinoSoundPath = "Sounds/Props/decepcao2-sfx-sino";
    string portaBarrasSoundPath = "Sounds/Props/decepcao1-sfx-porta-barras";
    string elevador2SoundPath = "Sounds/Props/decepcao2-sfx-elevador";
    string ponteQuebrandoSoundPath = "Sounds/Props/decepcao2-sfx-ponte-quebrando";
    string portaMadeiraSoundPath = "Sounds/Props/decepcao2-sfx-porta-madeira";
    string elevador3SoundPath = "Sounds/Props/decepcao3-sfx-elevador";
    string empurrandoRochasSoundPath = "Sounds/Props/decepcao2-sfx-empurrando-rochas";
    string memoriaCulto1SoundPath = "Sounds/Props/decepcao2-sfx-memoria-culto1";
    string memoriaCulto2SoundPath = "Sounds/Props/decepcao2-sfx-memoria-culto2";
    string chaveSoundPath = "Sounds/Props/Decepcao-sfx-pegando-chave";
    string estatuaQuebrandoSoundPath = "Sounds/Props/decepcao3-sfx-quebrando-estatua";

    void Start()
    {
        BufferSounds();

        soundController.PlayMusicContinuouslyByFileName(musicaSoundPath, true);
    }

    void LateUpdate()
    {
        //switch (state)
        //{
        //    case SoundState.SOLIDAO_1:
        //        PlayMusicaAmbiente1Solidao();
        //        break;
        //    case SoundState.SOLIDAO_3:
        //        PlayMusicaAmbiente3Solidao();
        //        break;
        //}
    }

    private void BufferSounds()
    {
        soundController = SoundController.Instance;

        //Implementados
        soundController.AddToBuffer(musicaSoundPath);
        soundController.AddToBuffer(sinoSoundPath);
        soundController.AddToBuffer(portaBarrasSoundPath);
        soundController.AddToBuffer(elevador2SoundPath);
        soundController.AddToBuffer(ponteQuebrandoSoundPath);
        soundController.AddToBuffer(portaMadeiraSoundPath);
        soundController.AddToBuffer(elevador3SoundPath);
        soundController.AddToBuffer(empurrandoRochasSoundPath);
        soundController.AddToBuffer(memoriaCulto1SoundPath);
        soundController.AddToBuffer(memoriaCulto2SoundPath);
        soundController.AddToBuffer(chaveSoundPath);
        soundController.AddToBuffer(estatuaQuebrandoSoundPath);
    }

    //Solidão
    public void PlaySino(AudioSource source) => soundController.PlayOnSourceByFileName(source, sinoSoundPath, true);
    public void PlayPortaBarras(AudioSource source) => soundController.PlayOnSourceByFileName(source, portaBarrasSoundPath, true);
    public void PlayElevador2(AudioSource source) => soundController.PlayOnSourceByFileName(source, elevador2SoundPath, true);
    public void PlayPonteQuebrando(AudioSource source) => soundController.PlayOnSourceByFileName(source, ponteQuebrandoSoundPath, true);
    public void PlayPortaMadeira(AudioSource source) => soundController.PlayOnSourceByFileName(source, portaMadeiraSoundPath, true);
    public void PlayElevador3(AudioSource source) => soundController.PlayOnSourceByFileName(source, elevador3SoundPath, true);
    public void PlayEmpurrandoRochas(AudioSource source) => soundController.PlayOnSourceByFileName(source, empurrandoRochasSoundPath, true);
    public void PlayMemoriaCulto1(AudioSource source) => soundController.PlayOnSourceByFileName(source, memoriaCulto1SoundPath, true);
    public void PlayMemoriaCulto2(AudioSource source) => soundController.PlayOnSourceByFileName(source, memoriaCulto2SoundPath, true);
    public void PlayChave(AudioSource source) => soundController.PlayOnSourceByFileName(source, chaveSoundPath, true);
    public void PlayEstatuaQuebrando(AudioSource source) => soundController.PlayOnSourceByFileName(source, estatuaQuebrandoSoundPath, true);

    public void PlayMusica() => soundController.PlayMusicTransitionByFileName(musicaSoundPath, true);
}
