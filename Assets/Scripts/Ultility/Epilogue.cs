using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;

public class Epilogue : MonoBehaviour
{
    [SerializeField] UnityEvent[] action;
    [SerializeField] float[] delay;
    [SerializeField] CinemachineVirtualCamera dollyCam;
    int idx = 0;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        EndGameMethod();
    }

    void EndGameMethod()
    {
        StartCoroutine(EndGame());
    }
    IEnumerator EndGame()
    {
        for (int i = 0; i < action.Length; i++)
        {
            Debug.Log("<b> [Epilogue]: </b>" + i);
            action[i].Invoke();
            yield return new WaitForSeconds(delay[i]);
        }
    }

    public void MoveDollyCam()
    {
        CinemachineTrackedDolly dCam = dollyCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        Debug.Log("<b> [Epilogue]: </b>" + dCam);
        DOTween.To(() => dCam.m_PathPosition, x => dCam.m_PathPosition = x, 2, 20).SetOptions(false);
    }

}
