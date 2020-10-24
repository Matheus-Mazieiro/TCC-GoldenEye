using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    float horizontal, vertical;
    float _horizontalRaw, _verticalRaw;
    [SerializeField] float speed;
    private float m_speed;
    [SerializeField] float jump;
    [HideInInspector] public CharacterController myCC;
    Vector3 direction = Vector3.zero;
    [SerializeField] float gravity;
    //DieInAir
    [SerializeField] float timeToDie;
    float m_timeToDie;

    [Header("Run")]
    [Range(1f, 5f)]
    [SerializeField] float runModifier = 2;
    [SerializeField] KeyCode runKey = KeyCode.LeftControl;
    bool isInGround;
    [HideInInspector] public bool m_isRunning;

    [Header("Crouch")]
    [Range(0f, 1f)]
    [SerializeField] float crouchModifier = .5f;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftShift;
    [SerializeField] Mesh ellipse;
    Mesh capsule;
    CapsuleCollider standCollider;
    SphereCollider crouchCollider;
    [HideInInspector] public bool m_isCrouching = false;


    [Header("Interact")]
    [Range(0f, 1f)]
    [SerializeField] float pushModifier = .2f;
    [SerializeField] KeyCode interactKey = KeyCode.LeftAlt;
    [HideInInspector] public GameObject box = null;
    [HideInInspector] public Handle handle = null;
    private bool m_isInteracting = false;
    [HideInInspector] public Transform elevator = null;

    AudioManager myAudioManager;
    int audioKit = 0;

    [SerializeField] bool loadPlayerPos;

    //Pause - TEMP
    [Header("Pause")]
    [SerializeField] GameObject pause;

    PlayerSoundConfig soundConfig;

    Coroutine isTired;

    public bool locked;

    private void Awake()
    {
        locked = false;

        if (loadPlayerPos)
        {
            PlayerData data = SaveSystem.LoadPlayer();
            if (data != null)
                transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }

        UnPause();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_speed = speed;
        myCC = GetComponent<CharacterController>();
        myAudioManager = GetComponent<AudioManager>();
        m_timeToDie = timeToDie;

        //Pause - TEMP
        Cursor.visible = false;

        //StartCoroutine(AutoSaver());
        soundConfig = GetComponent<PlayerSoundConfig>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        _horizontalRaw = Input.GetAxisRaw("Horizontal");
        _verticalRaw = Input.GetAxisRaw("Vertical");

        //Pause - TEMP
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        //Jump
        if (myCC.isGrounded && Input.GetKeyDown(KeyCode.Space) && !locked)
        {
            direction.y = jump;
            soundConfig.PlayPJPulo();
            GetComponent<AnimController>().SetJumpTrigger();
        }
        direction.x = horizontal * speed;
        direction.y -= gravity * Time.deltaTime;
        direction.y = Mathf.Max(direction.y, -10);
        direction.z = vertical * speed;

        //DieInAir
        if (!myCC.isGrounded)
        {
            m_timeToDie -= Time.deltaTime;
            if (m_timeToDie <= 0)
            {
                SceneFunctions scene = GameObject.FindObjectOfType<SceneFunctions>();
                if (scene)
                    scene.GoToScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            m_timeToDie = timeToDie;
        }

        //Run
        if (!m_isInteracting && !m_isCrouching)
        {
            if (Input.GetKeyDown(runKey))
            {
                speed = m_speed * runModifier;
                audioKit = 1;
                m_isRunning = true;
            }
            if (Input.GetKeyUp(runKey))
            {
                audioKit = 0;
                speed = m_speed;
                m_isRunning = false;
            }
        }


        //Crouch
        if (Input.GetKeyDown(crouchKey))
        {
            audioKit = 2;
            m_isCrouching = true;
            myCC.height = .5f;
            myCC.center = new Vector3(myCC.center.x, myCC.center.y - .75f, myCC.center.z);
            speed = m_speed * crouchModifier;
        }
        if (Input.GetKeyUp(crouchKey))
        {
            audioKit = 0;
            m_isCrouching = false;
            myCC.height = 2;
            myCC.center = new Vector3(myCC.center.x, myCC.center.y + .75f, myCC.center.z);
            speed = m_speed;
        }

        //Interact
        if (Input.GetKeyDown(interactKey))
        {
            if (handle)
            {
                handle.action.Invoke();
                handle = null;
            }
        }

        if (locked)
        {
            Vector3 _locked = Vector3.zero;

            if (direction.y < 0) _locked.y = direction.y;

            direction = _locked;

            if (m_isCrouching)
            {
                audioKit = 0;
                m_isCrouching = false;
                myCC.height = 2;
                myCC.center = new Vector3(myCC.center.x, myCC.center.y + .75f, myCC.center.z);
            }

            m_isRunning = false;
        }

        myCC.Move(direction * Time.deltaTime);

        if (_horizontalRaw != 0 || _verticalRaw != 0)
            transform.GetChild(0).LookAt(new Vector3(myCC.velocity.x + transform.position.x, transform.position.y - 1, myCC.velocity.z + transform.position.z));

        if (m_isCrouching)
        {
            if (myCC.velocity.x != 0 || myCC.velocity.z != 0) soundConfig.PlayPJAgachado();
            else soundConfig.PlayPJIdleAgachado();
        }

        else if (myCC.isGrounded)
        {
            if (myCC.velocity.x != 0 || myCC.velocity.z != 0)
            {
                if (m_isRunning)
                {
                    if (IsInvoking("CallTiredRunning")) CancelInvoke("CallTiredRunning");

                    soundConfig.PlayPJCorrendo();

                    Invoke("CallTiredRunning", 0.5f);
                }

                else soundConfig.PlayPJAndando();
            }

            else soundConfig.StopSFX();
        }

        else soundConfig.StopSFX();
    }

    void CallTiredRunning()
    {
        if (isTired != null) StopCoroutine(isTired);

        isTired = StartCoroutine(TiredRunning());
    }

    IEnumerator TiredRunning()
    {
        soundConfig.PlayPJIdle();

        yield return new WaitForSeconds(4.5f);

        soundConfig.StopPJIdle();
        isTired = null;
    }

    //Pause - TEMP
    public void Pause()
    {
        pause.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        pause.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ReloadScene(int sceneIndex)
    {
        GameObject.FindObjectOfType<SceneFunctions>().GoToScene(sceneIndex);
    }

    public void SetParentNull()
    {
        transform.parent = null;
    }

    //AutoSave
    public IEnumerator AutoSaver()
    {
        while (true)
        {
            if (isInGround)
            {
                SaveSystem.SavePlayer(transform);
                Debug.Log("Game saved");
            }

            yield return new WaitForSeconds(3f);
        }
    }
    public void SaveGame()
    {
        SaveSystem.SavePlayer(transform);
    }

    //Telepor player
    public void TeleportPlayerTo(Transform target)
    {
        Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Priority = 9;
        GameObject.Find("Camera").GetComponent<CinemachineVirtualCamera>().Priority = 10;

        myCC.enabled = false;
        transform.position = target.position;
        myCC.enabled = true;
    }
}
