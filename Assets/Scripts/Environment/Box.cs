using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<Movement>().box = gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            //if (other.GetComponent<Movement>().box == GetComponent<Rigidbody>())
            //{
            //   Debug.Log("pipipi");
            other.GetComponent<Movement>().box = null;
            transform.parent = null;
            //}
        }
    }
}
