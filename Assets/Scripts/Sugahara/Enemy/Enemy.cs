using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] Transform[] navPoints;
    [SerializeField] bool randomizeNavPoints;
    [SerializeField] bool awakeChasing;
    [SerializeField] bool distracted;
    [SerializeField] bool turnBack;
    [SerializeField] float distanceThreshold, speed, chaseSpeedMultiplier, searchDelay;

    [Header("Field of View")]
    [SerializeField] float maxAngle;
    [SerializeField] float maxDistance;
    [SerializeField] int rayCount, edgeIteration;

    [Header("Audio")]
    [SerializeField] float maxAudioDistance;

    public enum State { PATH, CHASE, SEARCH, STONE, STONED, PENDULUM, PENDULUMD }
    public State state { get; private set; }
    public Transform player { get; private set; }
    public string playerTag { get; private set; }

    bool firstTimeSeeing = true;

    SoundController soundController;
    AudioSource sfxSingle;
    AudioSource sfxLoop;

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
        if (state == State.PATH) soundController.PlayOnSourceContinuouslyByFileName(sfxLoop, "Sounds/J2/passos inimigo/inimigo andando", true);

        else if (state == State.CHASE)
        {
            if (firstTimeSeeing) soundController.PlayOnSourceByFileName(sfxSingle, "Sounds/J2/Grito inimigo/grito inimigo", true);

            firstTimeSeeing = false;
        }

        else if (state == State.SEARCH) soundController.PlayOnSourceContinuouslyByFileName(sfxLoop, "Sounds/J2/Procurando em Gavetas/procurando", true);

        else if (state == State.STONE)
        {
            soundController.PlayOnSourceByFileName(sfxSingle, "Sounds/J2/Batendo no inimigo (cenário)/batendo inimigo", true);

            state = State.STONED;
        }

        else if (state == State.PENDULUM)
        {
            soundController.PlayOnSourceByFileName(sfxSingle, "Sounds/J2/inimigo sendo acertado (Pêndulo)/Cópia de inimigo acertado 1", true);

            state = State.PENDULUMD;
        }
    }

    private void OnEnable()
    {
        if (awakeChasing)
        {
            state = State.CHASE;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) SceneManager.LoadScene(1);
    }

    private void BufferSounds()
    {
        soundController = SoundController.Instance;

        sfxSingle = gameObject.AddComponent<AudioSource>();
        sfxSingle.playOnAwake = false;
        sfxSingle.loop = false;
        sfxSingle.maxDistance = maxAudioDistance;
        sfxSingle.spatialBlend = 1;
        sfxSingle.rolloffMode = AudioRolloffMode.Linear;
        sfxSingle.priority = 64;

        sfxLoop = gameObject.AddComponent<AudioSource>();
        sfxLoop.playOnAwake = false;
        sfxLoop.loop = true;
        sfxLoop.maxDistance = maxAudioDistance;
        sfxLoop.spatialBlend = 1;
        sfxLoop.rolloffMode = AudioRolloffMode.Linear;
        sfxLoop.priority = 64;

        soundController.AddToBuffer("Sounds/J2/Grito inimigo/grito inimigo");
        soundController.AddToBuffer("Sounds/J2/passos inimigo/inimigo andando");
        soundController.AddToBuffer("Sounds/J2/Procurando em Gavetas/procurando");
        soundController.AddToBuffer("Sounds/J2/Batendo no inimigo (cenário)/batendo inimigo");
        soundController.AddToBuffer("Sounds/J2/inimigo sendo acertado (Pêndulo)/Cópia de inimigo acertado 1");

        //Mover estes sons pro Script do Jogador
        soundController.AddToBuffer("Sounds/J2/Alavanca/alavanca");
        soundController.AddToBuffer("Sounds/J2/Caixa-Objeto caindo/caixa caindo");
        soundController.AddToBuffer("Sounds/J2/Chão quebrando/-chao quebrando");
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
    public bool IsDistracted() => distracted;
    public bool TurnBack() => turnBack;

    public void SetState(State _state) => state = _state;
    public void DrawAttention(int delay) => StartCoroutine(RemoveDistracted(delay));

    public bool CompareState(State _state) => state == _state;

    IEnumerator RemoveDistracted(int delay)
    {
        yield return new WaitForSeconds(delay);

        distracted = false;
    }
}
