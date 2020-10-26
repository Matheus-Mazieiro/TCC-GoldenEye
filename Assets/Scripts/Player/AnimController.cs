using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimController : MonoBehaviour
{
    Animator anim;
    CharacterController myCC;
    Movement player;

    public Martelinho martelinho;
    public Animator porta_martelinho;
    public Animator mao_segura_Melki;
    public Animator mao_levanta_pedra;

    public Solidao_Controller solidao;
    public AudioSource porta_solidao;
    public AudioSource cavalinho_solidao;
    public AudioSource mao_solidao;

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

    public void PickUpHammer()
    {
        LockPlayer();

        anim.SetTrigger("pega_item");

        Invoke("UnlockPlayer", 1.15f);
    }

    public void HitHammer()
    {
        LockPlayer();

        anim.SetTrigger("martelinho");

        Invoke("BreakHammer", 0.9f);
    }

    void BreakHammer()
    {
        if (porta_martelinho)
        {
            porta_martelinho.Play("Take 001");
            if (martelinho) martelinho.BreakSelf();
            if (solidao) solidao.PlayPortaSolidao(porta_solidao);
        }

        Invoke("UnlockPlayer", 1.15f);
    }

    void LockPlayer()
    {
        player.locked = true;
    }

    void UnlockPlayer() => player.locked = false;

    public void CallAnimacaoFinalSolidao()
    {
        StartCoroutine(AnimacaoFinalSolidao());
    }

    IEnumerator AnimacaoFinalSolidao()
    {
        LockPlayer();

        if (solidao)
        {
            solidao.PlayMemoriasSolidao(cavalinho_solidao);
            solidao.PlayAghartaLevantaMelkiSolidao(mao_solidao);
        }

        if (mao_segura_Melki) mao_segura_Melki.Play("Take 001");

        yield return new WaitForSeconds(0.5f);

        anim.SetTrigger("surpresa");

        yield return new WaitForSeconds(3.5f);

        if (mao_levanta_pedra) mao_levanta_pedra.Play("Take 001");

        yield return new WaitForSeconds(8);

        player.locked = false;

        SceneManager.LoadScene(2);
    }
}
