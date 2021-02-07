using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zari20 : MonoBehaviour,Zari
{
    public Animator defaultAnim;
    public Animator pointAnim;
    public Animator point2Anim;
    public Animator attackingAnimator;
    public Animator defendingAnimator;
    public void StartMethod()
    {
        pointAnim.SetTrigger("Start");
        defaultAnim.SetTrigger("20zari");
        attackingAnimator.SetTrigger("20zari");
        defendingAnimator.SetTrigger("20zari");

        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(12);

        point2Anim.SetTrigger("Start");
    }
}
