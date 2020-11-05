using UnityEngine;
using UnityEngine.Events;

public class EnemyTriggerExit : MonoBehaviour
{
    public UnityEvent action;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10) action.Invoke();
    }
}
