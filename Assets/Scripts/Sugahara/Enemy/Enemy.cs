using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] Transform[] navPoints;
    [SerializeField] bool randomizeNavPoints;
    [SerializeField] float distanceThreshold, speed, chaseSpeedMultiplier, searchDelay;

    [Header("Field of View")]
    [SerializeField] float maxAngle;
    [SerializeField] float maxDistance;
    [SerializeField] int rayCount, edgeIteration;

    public enum State { PATH, CHASE, SEARCH, STONE, STONED, PENDULUM, PENDULUMD }
    public State state { get; private set; }
    public Transform player { get; private set; }
    public string playerTag { get; private set; }

    bool firstTimeSeeing = true;

    private void Awake()
    {
        Movement _player = FindObjectOfType<Movement>();
        if (_player) player = _player.transform;

        state = State.PATH;

        if (chaseSpeedMultiplier < 1) chaseSpeedMultiplier = 1;

        playerTag = "Player";

        BufferSounds();
    }

    private void Update()
    {
        if (state == State.PATH) SoundController.Instance.PlaySFXContinuouslyByFileName("Sounds/J2/passos inimigo/inimigo andando", true);

        else if (state == State.CHASE)
        {
            if (firstTimeSeeing) SoundController.Instance.PlaySingleSFXByFileName("Sounds/J2/Grito inimigo/grito inimigo", true);

            firstTimeSeeing = false;
        }

        else if (state == State.SEARCH) SoundController.Instance.PlaySFXContinuouslyByFileName("Sounds/J2/Procurando em Gavetas/procurando", true);

        else if (state == State.STONE)
        {
            SoundController.Instance.PlaySingleSFXByFileName("Sounds/J2/Batendo no inimigo (cenário)/batendo inimigo", true);

            state = State.STONED;
        }

        else if (state == State.PENDULUM)
        {
            SoundController.Instance.PlaySingleSFXByFileName("Sounds/J2/inimigo sendo acertado (Pêndulo)/Cópia de inimigo acertado 1", true);

            state = State.PENDULUMD;
        }
    }

    private void BufferSounds()
    {
        SoundController.Instance.AddToBuffer("Sounds/J2/Grito inimigo/grito inimigo");
        SoundController.Instance.AddToBuffer("Sounds/J2/passos inimigo/inimigo andando");
        SoundController.Instance.AddToBuffer("Sounds/J2/Procurando em Gavetas/procurando");
        SoundController.Instance.AddToBuffer("Sounds/J2/Batendo no inimigo (cenário)/batendo inimigo");
        SoundController.Instance.AddToBuffer("Sounds/J2/inimigo sendo acertado (Pêndulo)/Cópia de inimigo acertado 1");

        //Mover estes sons pro Script do Jogador
        SoundController.Instance.AddToBuffer("Sounds/J2/Alavanca/alavanca");
        SoundController.Instance.AddToBuffer("Sounds/J2/Caixa-Objeto caindo/caixa caindo");
        SoundController.Instance.AddToBuffer("Sounds/J2/Chão quebrando/-chao quebrando");
        SoundController.Instance.AddToBuffer("Sounds/J2/Goteira/goteira");
        SoundController.Instance.AddToBuffer("Sounds/J2/Movendo Caixa-Objeto/movendo");
        SoundController.Instance.AddToBuffer("Sounds/J2/movimentação PJ (provisória)/PJ agachado 3");
        SoundController.Instance.AddToBuffer("Sounds/J2/movimentação PJ (provisória)/PJ Andando 2");
        SoundController.Instance.AddToBuffer("Sounds/J2/movimentação PJ (provisória)/PJ correndo 1");
        SoundController.Instance.AddToBuffer("Sounds/J2/mão sendo construída/mao");
        SoundController.Instance.AddToBuffer("Sounds/J2/música medo/musica medo exploração");
        SoundController.Instance.AddToBuffer("Sounds/J2/música medo/musica medo perigo");
        SoundController.Instance.AddToBuffer("Sounds/J2/música medo/musica medo perseguição");
        SoundController.Instance.AddToBuffer("Sounds/J2/Pedras Rolando-Soltando/pedra rolando");
        SoundController.Instance.AddToBuffer("Sounds/J2/Pulo Pj (provisório)/pulo 1");
        SoundController.Instance.AddToBuffer("Sounds/J2/Pulo Pj (provisório)/pulo 2");
        SoundController.Instance.AddToBuffer("Sounds/J2/Sofrimento e Choro/choro");
        SoundController.Instance.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 1");
        SoundController.Instance.AddToBuffer("Sounds/J2/Sofrimento e Choro/sofrimento 2");
        SoundController.Instance.AddToBuffer("Sounds/J2/Som Ambiente caverna/som caverna 1");
        SoundController.Instance.AddToBuffer("Sounds/J2/Som Ambiente caverna/som caverna 2");
    }

    public Transform[] GetNavPoints() => navPoints;
    public bool GetRandomizeNavPoints() => randomizeNavPoints;
    public float GetDistanceThreshold() => distanceThreshold;
    public float GetSpeed() => speed;
    public float GetChaseSpeedMultiplier() => chaseSpeedMultiplier;
    public float GetSearchDelay() => searchDelay;
    public float GetMaxAngle() => maxAngle;
    public float GetMaxDistance() => maxDistance;
    public int GetRayCount() => rayCount;
    public int GetEdgeIteration() => edgeIteration;

    public void SetState(State _state) => state = _state;
    //public void SetPlayer(Transform _player) => player = _player;

    public bool CompareState(State _state) => state == _state;
}
