using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEnterTriggerEvent : MonoBehaviour
{
    public UnityEvent action;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
            action.Invoke();
    }

}
