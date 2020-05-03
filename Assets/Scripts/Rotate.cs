using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 rotateAngle;
    [SerializeField] float duration;

    public void RotateAngle(bool way)
    {
        transform.DORotate(transform.localEulerAngles + rotateAngle * (way ? 1 : -1), duration);
    }
}
