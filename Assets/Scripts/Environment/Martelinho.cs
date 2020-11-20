using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Martelinho : MonoBehaviour
{
    public AnimController player;
    [SerializeField] ParentConstraint constraint;

    private void Awake()
    {
        if(constraint == null)
            constraint = GetComponent<ParentConstraint>();
    }

    public void SetOnHand()
    {
        player.PickUpHammer();
        Invoke("PickUp", 1f);
    }

    void PickUp()
    {
        constraint.constraintActive = true;
    }

    public void BreakSelf()
    {
        constraint.constraintActive = false;
        gameObject.SetActive(false);
    }
}
