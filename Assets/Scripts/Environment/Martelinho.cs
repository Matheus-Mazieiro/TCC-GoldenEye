using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Martelinho : MonoBehaviour
{
    ParentConstraint constraint;

    private void Awake()
    {
        constraint = GetComponent<ParentConstraint>();
    }

    public void SetOnHand()
    {
        constraint.constraintActive = true;
    }

    public void BreakSelf()
    {
        constraint.constraintActive = false;
        gameObject.SetActive(false);
    }
}
