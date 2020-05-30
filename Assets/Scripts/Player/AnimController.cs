using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator anim;
    CharacterController myCC;
    Movement player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        myCC = GetComponent<CharacterController>();
        player = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2(myCC.velocity.x, myCC.velocity.z);
        anim.SetFloat("velocity", velocity.magnitude);

        anim.SetBool("agachar", player.m_isCrouching);

        anim.SetBool("correr", player.m_isRunning);

        anim.SetBool("chao", myCC.isGrounded);
    }

    public void SetJumpTrigger()
    {
        anim.SetTrigger("pulo");
    }
}
