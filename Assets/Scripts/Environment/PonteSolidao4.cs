using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonteSolidao4 : MonoBehaviour
{
    public AudioSource pedra;
    public AudioSource madeira;
    public Solidao_Controller solidao;

    public void TocarSonsAnimacao()
    {
        if (pedra)
        {
            solidao.PlayChaoPedraSolidao(pedra);
        }

        if (madeira)
        {
            Invoke("PlayChaoMadeira", 1.1f);
        }
    }

    void PlayChaoMadeira()
    {
        solidao.PlayChaoMadeiraSolidao(madeira);
    }
}
