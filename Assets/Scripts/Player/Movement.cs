using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    float horizontal, vertical;
    float _horizontalRaw, _verticalRaw;
    [SerializeField] float speed;
    private float m_speed;
    [SerializeField] float jump;
    Rigidbody myRb;

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

//#if UNITY_ENGINE
    private void Awake()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }
//#endif

    // Start is called before the first frame update
    void Start()
    {
        m_speed = speed;
        myRb = GetComponent<Rigidbody>();
        capsule = GetComponent<MeshFilter>().mesh;
        standCollider = GetComponent<CapsuleCollider>();
        crouchCollider = GetComponent<SphereCollider>();
        crouchCollider.enabled = false;
        myAudioManager = GetComponent<AudioManager>();

        //Pause - TEMP
        Cursor.visible = false;

        StartCoroutine(AutoSaver());
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.05f)
            && hit.collider.gameObject.layer != 9)
        {
            isInGround = true;
        }
        else StartCoroutine(CoyoteEffect());//isInGround = false;
        if (isInGround && Input.GetKeyDown(KeyCode.Space))
        {
            myRb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }

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
            GetComponent<MeshFilter>().mesh = ellipse;
            crouchCollider.enabled = true;
            standCollider.enabled = false;
            Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] += new Vector3(0, -.5f, 0);
            }
            GetComponent<MeshFilter>().mesh.vertices = vertices;
            speed = m_speed * crouchModifier;
        }
        if (Input.GetKeyUp(crouchKey))
        {
            audioKit = 0;
            m_isCrouching = false;
            GetComponent<MeshFilter>().mesh = capsule;
            crouchCollider.enabled = false;
            standCollider.enabled = true;
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
                //box.transform.SetParent(this.transform);
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
    }

    private void FixedUpdate()
    {
        myRb.velocity = new Vector3(horizontal * speed, myRb.velocity.y, vertical * speed);
        if(_horizontalRaw != 0 || _verticalRaw != 0)
            transform.GetChild(0).LookAt(new Vector3(myRb.velocity.x + myRb.position.x, myRb.position.y - 1, myRb.velocity.z + myRb.position.z));
    }

    //Efeito coiote
    IEnumerator CoyoteEffect()
    {
        yield return new WaitForSeconds(.1f);
        RaycastHit hit;
        if ((Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.05f)
            && hit.collider.gameObject.layer != 9) == false)
        {
            isInGround = false;
        }
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

    //Telepor player
    public void TeleportPlayerTo(Transform target)
    {
        myRb.position = target.position;
    }
}
