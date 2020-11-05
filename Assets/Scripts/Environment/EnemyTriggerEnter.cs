using UnityEngine;
using UnityEngine.Events;

public class EnemyTriggerEnter : MonoBehaviour
{
    public UnityEvent action;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10) action.Invoke();
    }
}
