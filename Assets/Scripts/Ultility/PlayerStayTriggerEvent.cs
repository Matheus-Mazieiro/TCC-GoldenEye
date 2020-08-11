using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStayTriggerEvent : MonoBehaviour
{
    public UnityEvent action;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && other.CompareTag("Player"))
            action.Invoke();
    }
}
