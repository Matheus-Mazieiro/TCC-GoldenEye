using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundConfig : MonoBehaviour
{
    SoundController soundController;

    void Awake()
    {
        BufferSounds();
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

        //Nao implementados
        soundController.AddToBuffer("Sounds/J2/Caixa-Objeto caindo/caixa caindo");
        soundController.AddToBuffer("Sounds/J2/Goteira/goteira");
        soundController.AddToBuffer("Sounds/J2/Movendo Caixa-Objeto/movendo");
        soundController.AddToBuffer("Sounds/J2/movimentação PJ (provisória)/PJ agachado 3");
        soundController.AddToBuffer("Sounds/J2/movimentação PJ (provisória)/PJ Andando 2");
        soundController.AddToBuffer("Sounds/J2/movimentação PJ (provisória)/PJ correndo 1");
        soundController.AddToBuffer("Sounds/J2/mão sendo construída/mao");
        soundController.AddToBuffer("Sounds/J2/música medo/musica medo exploração");
        soundController.AddToBuffer("Sounds/J2/música medo/musica medo perigo");
        soundController.AddToBuffer("Sounds/J2/música medo/musica medo perseguição");
        soundController.AddToBuffer("Sounds/J2/Pedras Rolando-Soltando/pedra rolando");
        soundController.AddToBuffer("Sounds/J2/Pulo Pj (provisório)/pulo 1");
        soundController.AddToBuffer("Sounds/J2/Pulo Pj (provisório)/pulo 2");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/choro");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 1");
        soundController.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 2");
        soundController.AddToBuffer("Sounds/J2/Som Ambiente caverna/som caverna 1");
        soundController.AddToBuffer("Sounds/J2/Som Ambiente caverna/som caverna 2");
    }

    public void PlayAlavanca() => soundController.PlaySingleSFXByFileName("Sounds/J2/Alavanca/alavanca", true);

    public void PlayChaoQuebrando() => soundController.PlaySingleSFXByFileName("Sounds/J2/Chao quebrando/chao quebrando", true);
}
