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
    [SerializeField] int stepSoundType;

    public enum State { PATH, CHASE, SEARCH, STONE, STONED, PENDULUM, PENDULUMD }
    public State state { get; private set; }
    public Transform player { get; private set; }
    public string playerTag { get; private set; }

    bool firstTimeSeeing = true;

    string stepSoundPath = "Sounds/Enemies/inimigo 1 andando";
    string runningSoundPath = "Sounds/Enemies/inimigo 1 correndo (baixinho)";
    string idleSoundPath = "Sounds/Enemies/inimigo 1 idle olhando";
    string gritoSoundPath = "Sounds/Enemies/grito inimigo";
    string procurandoSoundPath = "Sounds/Enemies/procurando";
    string acertandoInimigoSoundPath = "Sounds/Enemies/acertando inimigo";
    string batendoInimigoSoundPath = "Sounds/Enemies/batendo inimigo";

    SoundController soundController;
    AudioSource sfxSingle;
    AudioSource sfxLoop;

    PlayerSoundConfig playerSound;

    [Header("Animation")]
    [SerializeField] Transform animator;


    private void Awake()
    {
        playerSound = FindObjectOfType<PlayerSoundConfig>();

        Movement _player = FindObjectOfType<Movement>();
        if (_player) player = _player.transform;

        state = State.PATH;

        if (chaseSpeedMultiplier < 1) chaseSpeedMultiplier = 1;

        playerTag = "Player";

        BufferSounds();

        if (stepSoundType == 1)
        {
            stepSoundPath = "Sounds/Enemies/inimigo 1 andando";
            runningSoundPath = "Sounds/Enemies/inimigo 1 correndo (baixinho)";
            idleSoundPath = "Sounds/Enemies/inimigo 1 idle olhando";
        }

        else if (stepSoundType == 2)
        {
            stepSoundPath = "Sounds/Enemies/inimigo 2 andando";
            runningSoundPath = "Sounds/Enemies/inimigo 2 correndo (alto)";
            idleSoundPath = "Sounds/Enemies/Inimigo 2 idle olhando";
        }
        
        else if (stepSoundType == 3)
        {
            stepSoundPath = "Sounds/Enemies/inimigo 3 andando (bruta monte)";
            runningSoundPath = "Sounds/Enemies/inimigo 2 correndo (alto)";
        }
    }

    private void Update()
    {
        if (!playerSound.IsMedoCaverna()) playerSound.SetMedoExploracao();

        if (state == State.PATH) soundController.PlayOnSourceContinuouslyByFileName(sfxLoop, stepSoundPath, true);

        else if (state == State.CHASE)
        {
            if (firstTimeSeeing) soundController.PlayOnSourceByFileName(sfxSingle, gritoSoundPath, true);

            firstTimeSeeing = false;

            soundController.PlayOnSourceContinuouslyByFileName(sfxLoop, runningSoundPath, true);

            playerSound.SetMedoPerseguicao();
        }

        else if (state == State.SEARCH) soundController.PlayOnSourceContinuouslyByFileName(sfxLoop, procurandoSoundPath, true);

        else if (state == State.STONE)
        {
            soundController.PlayOnSourceByFileName(sfxSingle, batendoInimigoSoundPath, true);

            state = State.STONED;
        }

        else if (state == State.PENDULUM)
        {
            soundController.PlayOnSourceByFileName(sfxSingle, acertandoInimigoSoundPath, true);

            state = State.PENDULUMD;
        }

        if (Vector3.Distance(transform.position, player.position) < 30)
        {
            playerSound.SetMedoPerigo();
        }

        //Animation
        if (animator)
        {
            Debug.Log(state);

            if (state == State.PATH)
            {
                if (animator.GetChild(0).gameObject.activeInHierarchy == false)
                {
                    animator.GetChild(0).gameObject.SetActive(true);
                    animator.GetChild(1).gameObject.SetActive(false);
                    animator.GetChild(2).gameObject.SetActive(false);
                }
            }
            else if (state == State.CHASE)
            {
                if (animator.GetChild(1).gameObject.activeInHierarchy == false)
                {
                    animator.GetChild(0).gameObject.SetActive(false);
                    animator.GetChild(1).gameObject.SetActive(true);
                    animator.GetChild(2).gameObject.SetActive(false);
                }
            }
            else if (animator.GetChild(2).gameObject.activeInHierarchy == false)
            {
                animator.GetChild(0).gameObject.SetActive(false);
                animator.GetChild(1).gameObject.SetActive(false);
                animator.GetChild(2).gameObject.SetActive(true);
            }
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

        else if (collision.gameObject.CompareTag("Obstacle")) Distract(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) Distract(0);
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

        soundController.AddToBuffer(stepSoundPath);
        soundController.AddToBuffer(runningSoundPath);
        soundController.AddToBuffer(gritoSoundPath);
        soundController.AddToBuffer(procurandoSoundPath);
        soundController.AddToBuffer(batendoInimigoSoundPath);
        soundController.AddToBuffer(acertandoInimigoSoundPath);
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
    public void Distract(int delay) => StartCoroutine(ApplyDistracted(delay));

    public bool CompareState(State _state) => state == _state;

    IEnumerator RemoveDistracted(int delay)
    {
        yield return new WaitForSeconds(delay);

        distracted = false;
    }

    IEnumerator ApplyDistracted(int delay)
    {
        awakeChasing = false;
        playerSound.SetMedoExploracao();

        yield return new WaitForSeconds(delay);

        state = State.PATH;
        distracted = true;
        //player = null;
    }
}
