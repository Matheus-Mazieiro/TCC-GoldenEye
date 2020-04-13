using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerExitTriggerEvent : MonoBehaviour
{
    public UnityEvent action;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
            action.Invoke();
    }
}
