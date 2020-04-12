using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    float horizontal, vertical;
    float speed;
    [SerializeField] float walkingSpeed;
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

    [Header("Interact")]
    [Range(0f, 1f)]
    [SerializeField] float pushModifier = .2f;
    [SerializeField] KeyCode interactKey = KeyCode.E;
    /*[HideInInspector]*/
    public GameObject box = null;
    [HideInInspector] public Handle handle = null;

    public GameObject interactMessage;

    // Start is called before the first frame update
    void Start()
    {
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
            if (!hit.collider.CompareTag("Player")) isInGround = true;
            else isInGround = false;
        }

        else isInGround = false;

        if (isInGround && Input.GetKeyDown(KeyCode.Space) && !interacting)
        {
            myRb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }

        //Run
        if (isInGround && Input.GetKeyDown(runKey) && !interacting)
            speed = walkingSpeed * runModifier;
        if (isInGround && Input.GetKeyUp(runKey) && !interacting)
            speed = walkingSpeed;

        //Crouch
        if (Input.GetKeyDown(crouchKey) && !interacting)
        {
            GetComponent<MeshFilter>().mesh = ellipse;
            crouchCollider.enabled = true;
            standCollider.enabled = false;
            Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] += new Vector3(0, -.5f, 0);
            }
            GetComponent<MeshFilter>().mesh.vertices = vertices;
            speed = walkingSpeed * crouchModifier;
        }
        if (Input.GetKeyUp(crouchKey) && !interacting)
        {
            GetComponent<MeshFilter>().mesh = capsule;
            crouchCollider.enabled = false;
            standCollider.enabled = true;
            speed = walkingSpeed;
        }

        //Interact
        if (Input.GetKeyDown(interactKey))
        {
            interactMessage.SetActive(false);

            if (box)
            {
                speed = walkingSpeed * pushModifier;
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
                speed = walkingSpeed;
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
