using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float distance = .85f;
    public float distanceThreshold = .9f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            other.GetComponent<Movement>().box = gameObject;
        }
    }

    private void Update()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, distance))
        {
            transform.Translate(Vector3.down * Time.deltaTime * .5f);
        } else if(!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, distance * distanceThreshold))
        {
            transform.Translate(Vector3.down * Time.deltaTime * -.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<Movement>().box = null;
        }
    }
}
