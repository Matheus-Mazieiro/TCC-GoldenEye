using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Prelogue : MonoBehaviour
{
    [SerializeField] UnityEvent action;
    [SerializeField] UnityEvent cmAction;
    [SerializeField] Image blackScreen;
    [SerializeField] Image yearsAfter;
    [SerializeField] AudioSource heartBeat;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(13);
        DOTween.To(() => blackScreen.color, x => blackScreen.color = x, new Color(0, 0, 0, 0), 7);
        yield return new WaitForSeconds(2);
        action.Invoke();
        cmAction.Invoke();
        heartBeat.Play();
        yield return new WaitForSeconds(11);
        DOTween.To(() => blackScreen.color, x => blackScreen.color = x, new Color(0, 0, 0, 1), 2);
        yield return new WaitForSeconds(3);
        DOTween.To(() => yearsAfter.color, x => yearsAfter.color = x, new Color(0.81132080f, 0.8113208f, 0.8113208f, 1), 3);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);

    }
}
