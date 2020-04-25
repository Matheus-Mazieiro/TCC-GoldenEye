using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Elevator : MonoBehaviour
{
    public Vector3 moveTo;
    public float duration;
    Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    public void GOUp(bool subir)
    {
        transform.DOMove(initPos + moveTo * (subir ? 1 : 0), duration, false);
    }
}