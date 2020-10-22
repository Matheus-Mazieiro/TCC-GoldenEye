using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator anim;

    private void Awake()
    {
        if (anim == null) anim = GetComponent<Animator>();
    }

    public void PlayBool(string boolean) => anim.SetBool(boolean, true);
    public void StopBool(string boolean) => anim.SetBool(boolean, false);
}
