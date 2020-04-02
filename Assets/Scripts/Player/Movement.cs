using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    float horizontal, vertical;
    [SerializeField] float speed;
    [SerializeField] float jump;
    Rigidbody myRb;

    [Header("Run")]
    [Range(1f, 5f)]
    [SerializeField] float runModifier = 2;
    [SerializeField] KeyCode runKey = KeyCode.LeftControl;

    [Header("Crouch")]
    [Range(0f, 1f)]
    [SerializeField] float crouchModifier = .5f;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftShift;
    [SerializeField] Mesh ellipse;
    Mesh capsule;
    CapsuleCollider standCollider;
    SphereCollider crouchCollider;

    [Header("Interact")]
    [Range(0f, 1f)]
    [SerializeField] float pushModifier = .2f;
    [SerializeField] KeyCode interactKey = KeyCode.LeftAlt;
    [HideInInspector] public GameObject box = null;
    [HideInInspector] public Handle handle = null;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        capsule = GetComponent<MeshFilter>().mesh;
        standCollider = GetComponent<CapsuleCollider>();
        crouchCollider = GetComponent<SphereCollider>();
        crouchCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            myRb.AddForce(Vector3.up * jump, ForceMode.Impulse);

        //Correr
        if (Input.GetKeyDown(runKey))
            speed *= runModifier;
        if (Input.GetKeyUp(runKey))
            speed /= runModifier;


        //Agachar
        if (Input.GetKeyDown(crouchKey))
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
            speed *= crouchModifier;
        }
        if (Input.GetKeyUp(crouchKey))
        {
            GetComponent<MeshFilter>().mesh = capsule;
            crouchCollider.enabled = true;
            standCollider.enabled = false;
            speed /= crouchModifier;
        }

        //Interagir
        if (Input.GetKeyDown(interactKey))
        {
            if (box)
            {
                speed *= pushModifier;
                box.transform.parent = this.transform;
            } else if (handle)
            {
                handle.action.Invoke();
                handle = null;
            }
        }
        if (Input.GetKeyUp(interactKey))
        {
            if (box)
            {
                speed /= pushModifier;
                box.transform.parent = null;
            }
        }
    }

    private void FixedUpdate()
    {
        myRb.velocity = new Vector3(horizontal * speed, myRb.velocity.y, vertical * speed);
    }
}
