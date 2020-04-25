using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float distance = .85f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            other.GetComponent<Movement>().box = gameObject;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        //RaycastHit hit2;
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, distance))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 10);
        }//else if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit2, .7f))
        //{
        //    transform.Translate(Vector3.down * Time.deltaTime * -1);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<Movement>().box = null;
        }
    }
}
