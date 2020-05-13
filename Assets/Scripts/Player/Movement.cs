using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    float horizontal, vertical;
    float _horizontalRaw, _verticalRaw;
    [SerializeField] float speed;
    private float m_speed;
    [SerializeField] float jump;
    CharacterController myCC;
    Vector3 direction = Vector3.zero;
    [SerializeField] float gravity;

    [Header("Run")]
    [Range(1f, 5f)]
    [SerializeField] float runModifier = 2;
    [SerializeField] KeyCode runKey = KeyCode.LeftControl;
    bool isInGround;

    [Header("Crouch")]
    [Range(0f, 1f)]
    [SerializeField] float crouchModifier = .5f;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftShift;
    [SerializeField] Mesh ellipse;
    Mesh capsule;
    CapsuleCollider standCollider;
    SphereCollider crouchCollider;
    private bool m_isCrouching = false;


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

    //Pause - TEMP
    [Header("Pause")]
    [SerializeField] GameObject pause;

#if UNITY_ENGINE
    private void Awake()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }
#endif

    // Start is called before the first frame update
    void Start()
    {
        m_speed = speed;
        myCC = GetComponent<CharacterController>();
        myAudioManager = GetComponent<AudioManager>();

        //Pause - TEMP
        Cursor.visible = false;

        //StartCoroutine(AutoSaver());
        Time.timeScale = 1;
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
        if (myCC.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            direction.y = jump;
        }
        direction.x = horizontal * speed;
        direction.y -= gravity * Time.deltaTime;
        direction.y = Mathf.Max(direction.y, -10);
        direction.z = vertical * speed;

        //Run
        if (!m_isInteracting && !m_isCrouching)
        {
            if (Input.GetKeyDown(runKey))
            {
                speed = m_speed * runModifier;
                audioKit = 1;
            }
            if (Input.GetKeyUp(runKey))
            {
                audioKit = 0;
                speed = m_speed;
            }
        }


        //Crouch
        if (Input.GetKeyDown(crouchKey))
        {
            audioKit = 2;
            m_isCrouching = true;
            myCC.height = 1;
            myCC.center = new Vector3(myCC.center.x, myCC.center.y - .5f, myCC.center.z);
            speed = m_speed * crouchModifier;
        }
        if (Input.GetKeyUp(crouchKey))
        {
            audioKit = 0;
            m_isCrouching = false;
            myCC.height = 2;
            myCC.center = new Vector3(myCC.center.x, myCC.center.y + .5f, myCC.center.z);
            speed = m_speed;
        }

        //Interact
        if (Input.GetKeyDown(interactKey))
        {
            if (!m_isCrouching && box)
            {
                audioKit = 3;
                box.layer = 9;

                m_isInteracting = true;
                speed = m_speed * pushModifier;
                box.transform.parent = this.transform;
            }
            else if (handle)
            {
                handle.action.Invoke();
                handle = null;
            }
        }
        if (Input.GetKeyUp(interactKey))
        {
            audioKit = 0;
            box.layer = 0;

            m_isInteracting = false;
            speed = m_speed;


            foreach (Transform box in transform)
            {
                if (!box.CompareTag("Player"))
                {
                    foreach (BoxCollider item in box.GetComponents<BoxCollider>())
                    {
                        if (!item.isTrigger)
                            item.enabled = true;
                    }
                    if (elevator)
                        box.transform.parent = elevator;
                    else box.transform.parent = null;
                }
            }

        }

        myCC.Move(direction * Time.deltaTime);
        if(_horizontalRaw != 0 || _verticalRaw != 0)
            transform.GetChild(0).LookAt(new Vector3(myCC.velocity.x + transform.position.x, transform.position.y - 1, myCC.velocity.z + transform.position.z));
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

            yield return new WaitForSeconds (3f);
        }
    }
    public void SaveGame()
    {
        SaveSystem.SavePlayer(transform);
    }

    //Telepor player
    public void TeleportPlayerTo(Transform target)
    {
        Debug.Log("Teleportou");

        Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Priority = 9;
        GameObject.Find("Camera").GetComponent<CinemachineVirtualCamera>().Priority = 10;

        myCC.enabled = false;
        transform.position = target.position;
        myCC.enabled = true;
    }
}
