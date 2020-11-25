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
    [SerializeField] Image blackScreen;
    [SerializeField] Image credits;
    [SerializeField] Image thanks;
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
            action[i].Invoke();
            yield return new WaitForSeconds(delay[i]);
        }
    }

    public void MoveDollyCam()
    {
        CinemachineTrackedDolly dCam = dollyCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        DOTween.To(() => dCam.m_PathPosition, x => dCam.m_PathPosition = x, 2, 20).SetOptions(false);
    }

    public void BlackFadeIn(float time)
    {
        DOTween.To(() => blackScreen.color, x => blackScreen.color = x, new Color(0, 0, 0, 1), time);
    }
    public void CreditFadeIn(float time)
    {
        DOTween.To(() => credits.color, x => credits.color = x, new Color(1, 1, 1, 1), time);
    }
    public void CreditFadeOut(float time)
    {
        DOTween.To(() => credits.color, x => credits.color = x, new Color(1, 1, 1, 0), time);
    }
    public void ThanksFadeIn(float time)
    {
        DOTween.To(() => thanks.color, x => thanks.color = x, new Color(1, 1, 1, 1), time);
    }
    public void ThanksFadeOut(float time)
    {
        DOTween.To(() => thanks.color, x => thanks.color = x, new Color(1, 1, 1, 0), time);
    }
}
