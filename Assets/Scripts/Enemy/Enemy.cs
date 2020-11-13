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
    [SerializeField] Transform lookAt;

    [Header("Field of View")]
    [SerializeField] float maxAngle;
    [SerializeField] float maxDistance;
    [SerializeField] int rayCount, edgeIteration;

    [Header("Audio")]
    [SerializeField] float maxAudioDistance;
    [SerializeField] int stepSoundType;

    public enum State { PATH, CHASE, SEARCH, STONE, STONED, PENDULUM, PENDULUMD, CHECKING }
    public State state { get; private set; }
    public Transform player { get; private set; }
    public string playerTag { get; private set; }

    bool firstTimeSeeing = true;
    bool setDanger = false;
    bool setChase = false;

    string stepSoundPath = "Sounds/Enemies/inimigo 1 andando";
    string runningSoundPath = "Sounds/Enemies/inimigo 1 correndo (baixinho)";
    string idleSoundPath = "Sounds/Enemies/inimigo 1 idle olhando";
    string gritoSoundPath = "Sounds/Enemies/grito inimigo";
    string procurandoSoundPath = "Sounds/Enemies/procurando";
    string acertandoInimigoSoundPath = "Sounds/Enemies/acertando inimigo";
    string batendoInimigoSoundPath = "Sounds/Enemies/batendo inimigo";

    AudioSource sfxSingle;
    AudioSource sfxLoop;

    Medo_Controller medoController;

    [Header("Animation")]
    [SerializeField] Transform animator;


    private void Awake()
    {
        medoController = FindObjectOfType<Medo_Controller>();

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
        if (state == State.PATH) SoundController.Instance.PlayOnSourceContinuouslyByFileName(sfxLoop, stepSoundPath, true);

        else if (state == State.CHASE)
        {
            if (firstTimeSeeing) SoundController.Instance.PlayOnSourceByFileName(sfxSingle, gritoSoundPath, true);

            firstTimeSeeing = false;

            SoundController.Instance.PlayOnSourceContinuouslyByFileName(sfxLoop, runningSoundPath, true);

            if (medoController) medoController.SetMedoPerseguicao();
            setChase = true;
        }

        else if (state == State.SEARCH) SoundController.Instance.PlayOnSourceContinuouslyByFileName(sfxLoop, procurandoSoundPath, true);

        else if (state == State.STONE)
        {
            SoundController.Instance.PlayOnSourceByFileName(sfxSingle, batendoInimigoSoundPath, true);

            state = State.STONED;
        }

        else if (state == State.PENDULUM)
        {
            SoundController.Instance.PlayOnSourceByFileName(sfxSingle, acertandoInimigoSoundPath, true);

            state = State.PENDULUMD;
        }

        if (state != State.CHASE && setChase && medoController)
        {
            medoController.SetMedoExploracao();
            setChase = false;
        }

        if (Vector3.Distance(transform.position, player.position) < 30)
        {
            if (medoController) medoController.SetMedoPerigo();
            setDanger = true;
        }

        else if (setDanger)
        {
            if (medoController) medoController.SetMedoExploracao();
            setDanger = false;
        }

        //Animation
        if (animator)
        {
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
        if (collision.gameObject.CompareTag("Player")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        else if (collision.gameObject.CompareTag("Obstacle")) Distract(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) Distract(0);
    }

    private void BufferSounds()
    {
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

        SoundController.Instance.AddToBuffer(stepSoundPath);
        SoundController.Instance.AddToBuffer(runningSoundPath);
        SoundController.Instance.AddToBuffer(gritoSoundPath);
        SoundController.Instance.AddToBuffer(procurandoSoundPath);
        SoundController.Instance.AddToBuffer(batendoInimigoSoundPath);
        SoundController.Instance.AddToBuffer(acertandoInimigoSoundPath);
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

    public void LookAt()
    {
        Debug.Log(!!lookAt);
        if (lookAt) transform.LookAt(lookAt);
    }

    IEnumerator RemoveDistracted(int delay)
    {
        yield return new WaitForSeconds(delay);

        distracted = false;
    }

    IEnumerator ApplyDistracted(int delay)
    {
        awakeChasing = false;

        yield return new WaitForSeconds(delay);

        state = State.PATH;
        distracted = true;

        if (medoController) medoController.SetMedoPerigo();
    }
}
