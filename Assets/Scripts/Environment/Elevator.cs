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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
            collision.gameObject.GetComponent<Movement>().elevator = transform;
        //collision.transform.SetParent(transform);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
            collision.gameObject.GetComponent<Movement>().elevator = null;
        //collision.transform.SetParent(null);
    }

    public void GOUp(bool subir)
    {
        transform.DOMove(initPos + moveTo * (subir ? 1 : 0), duration, false).SetEase(Ease.InSine);
    }
    public void MoveGameObject(Transform whoMoves)
    {
        whoMoves.DOMove(whoMoves.position + moveTo, duration);
    }
}