using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Rigidbody parentRb;
    public float gravityScale = 10f;
    Transform enemy;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().isTrigger = true;
        parentRb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (parentRb.constraints != RigidbodyConstraints.FreezeAll)
        {
            Vector3 gravity = gravityScale * 9.81f * -Vector3.up;
            parentRb.AddForce(gravity, ForceMode.Acceleration);
            if (enemy)
            {
                parentRb.position = new Vector3(enemy.position.x, parentRb.position.y, enemy.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            parentRb.constraints = RigidbodyConstraints.None;
            enemy = other.transform;
        }
    }
}
