using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Elevator : MonoBehaviour
{
    public Vector3 moveTo;
    public float duration;
    public AudioSource initialSound;
    Vector3 initPos;

    Solidao_Controller solidaoController;
    AudioSource source;
    public Animator manivela;

    private void Start()
    {
        initPos = transform.position;

        solidaoController = FindObjectOfType<Solidao_Controller>();
        source = GetComponent<AudioSource>();
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
        if (manivela)
        {
            if (subir) manivela.SetTrigger("Start");
            else manivela.SetTrigger("Reverse");
        }

        if (initialSound) solidaoController.PlayRoldanaSolidao(initialSound);

        if (source)
        {
            solidaoController.PlayEngrenagem2Solidao(source);
            Invoke("StopAudioSource", duration);
        }
        transform.DOMove(initPos + moveTo * (subir ? 1 : 0), duration, false).SetEase(Ease.InSine);
    }
    public void MoveGameObject(Transform whoMoves)
    {
        whoMoves.DOMove(whoMoves.position + moveTo, duration);
    }

    private void StopAudioSource()
    {
        source.Stop();

        if (manivela) manivela.SetTrigger("Stop");
    }
}