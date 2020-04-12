using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    AudioManager myAudioManager;

    [Header("Movement")]
    float horizontal, vertical;
<<<<<<< HEAD
    float speed;
    [SerializeField] float walkingSpeed;
=======
    [SerializeField] float speed;
    private float m_speed;
>>>>>>> 3866019e87f561a4587b2487db1a5ccd379c8223
    [SerializeField] float jump;
    Rigidbody myRb;

    [Header("Run")]
    [Range(1f, 5f)]
    [SerializeField] float runModifier = 2;
    [SerializeField] KeyCode runKey = KeyCode.LeftShift;
    bool isInGround;

    [Header("Crouch")]
    [Range(0f, 1f)]
    [SerializeField] float crouchModifier = .5f;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] Mesh ellipse;
    Mesh capsule;
    CapsuleCollider standCollider;
    SphereCollider crouchCollider;
    private bool m_isCrouching;

    [Header("Interact")]
    [Range(0f, 1f)]
    [SerializeField] float pushModifier = .2f;
<<<<<<< HEAD
    [SerializeField] KeyCode interactKey = KeyCode.E;
    /*[HideInInspector]*/
    public GameObject box = null;
=======
    [SerializeField] KeyCode interactKey = KeyCode.LeftAlt;
    private bool m_isHolding;
    [HideInInspector] public GameObject box = null;
>>>>>>> 3866019e87f561a4587b2487db1a5ccd379c8223
    [HideInInspector] public Handle handle = null;

    public GameObject interactMessage;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        myAudioManager = GetComponent<AudioManager>();
        m_speed = speed;
        myRb = GetComponent<Rigidbody>();
        capsule = GetComponent<MeshFilter>().mesh;
        standCollider = GetComponent<CapsuleCollider>();
        crouchCollider = GetComponent<SphereCollider>();
        crouchCollider.enabled = false;
        speed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        bool interacting = Input.GetKey(interactKey);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //Jump
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.05f))
        {
<<<<<<< HEAD
            if (!hit.collider.CompareTag("Player")) isInGround = true;
            else isInGround = false;
        }

        else isInGround = false;

        if (isInGround && Input.GetKeyDown(KeyCode.Space) && !interacting)
=======
            myRb.mass = 1;
            isInGround = true;
        }
        else
        {
            isInGround = false;
            if (myRb.velocity.y <= 0)
                myRb.velocity = new Vector3(myRb.velocity.x, myRb.velocity.y * 1.07f, myRb.velocity.z);
        }
        if (isInGround && Input.GetKeyDown(KeyCode.Space))
>>>>>>> 3866019e87f561a4587b2487db1a5ccd379c8223
        {
            myRb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }

        //Run
<<<<<<< HEAD
        if (isInGround && Input.GetKeyDown(runKey) && !interacting)
            speed = walkingSpeed * runModifier;
        if (isInGround && Input.GetKeyUp(runKey) && !interacting)
            speed = walkingSpeed;
=======
        if (!m_isHolding && !m_isCrouching)
        {
            if (Input.GetKeyDown(runKey))
            {
                speed = m_speed * runModifier;
            }
            if (Input.GetKeyUp(runKey))
            {
                speed = m_speed;// / runModifier;
            }
        }

>>>>>>> 3866019e87f561a4587b2487db1a5ccd379c8223

        //Crouch
        if (Input.GetKeyDown(crouchKey) && !interacting)
        {
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
<<<<<<< HEAD
            speed = walkingSpeed * crouchModifier;
=======
            speed = m_speed * crouchModifier;
>>>>>>> 3866019e87f561a4587b2487db1a5ccd379c8223
        }
        if (Input.GetKeyUp(crouchKey) && !interacting)
        {
            m_isCrouching = false;
            GetComponent<MeshFilter>().mesh = capsule;
            crouchCollider.enabled = false;
            standCollider.enabled = true;
<<<<<<< HEAD
            speed = walkingSpeed;
=======
            speed = m_speed;// / crouchModifier;
>>>>>>> 3866019e87f561a4587b2487db1a5ccd379c8223
        }

        //Interact
        if (Input.GetKeyDown(interactKey))
        {
            interactMessage.SetActive(false);

            if (box)
            {
<<<<<<< HEAD
                speed = walkingSpeed * pushModifier;
=======
                m_isHolding = true;
                speed = m_speed * pushModifier;
>>>>>>> 3866019e87f561a4587b2487db1a5ccd379c8223
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
            if (box)
            {
<<<<<<< HEAD
                speed = walkingSpeed;
=======
                m_isHolding = false;
                speed = m_speed;// / pushModifier;
>>>>>>> 3866019e87f561a4587b2487db1a5ccd379c8223
                box.transform.parent = null;
            }
        }

        //Box
        //if(holdingBox)

        if ((box || handle) && !interacting)
        {
            Vector3 position = box ? Camera.main.WorldToViewportPoint(box.transform.position) : Camera.main.WorldToViewportPoint(handle.transform.position);
            position.y += 0.15f;

            interactMessage.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(position);
            interactMessage.SetActive(true);
        }

        if (!box && !handle && interactMessage.activeSelf) interactMessage.SetActive(false);
    }

    private void FixedUpdate()
    {
        myRb.velocity = new Vector3(horizontal * speed, myRb.velocity.y, vertical * speed);
    }
}
