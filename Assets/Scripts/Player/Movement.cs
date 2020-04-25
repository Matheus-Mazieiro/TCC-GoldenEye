using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    float horizontal, vertical;
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

    AudioManager myAudioManager;
    int audioKit = 0;

    //Pause - TEMP
    [Header("Pause")]
    [SerializeField] GameObject pause;

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
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

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
                foreach (BoxCollider item in box.GetComponents<BoxCollider>())
                {
                    if (!item.isTrigger)
                        item.enabled = true;
                }

                box.transform.parent = transform.parent;
            }
        }



        //if (horizontal != 0 || vertical != 0)
        //    myAudioManager.PlayAudio(audioKit, false, true);
        //else myAudioManager.StopAudio();
    }

    private void FixedUpdate()
    {
        myRb.velocity = new Vector3(horizontal * speed, myRb.velocity.y, vertical * speed);
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
}
