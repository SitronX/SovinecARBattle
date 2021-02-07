using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zari14 : MonoBehaviour, Zari
{
    public Animator anim;
    public void StartMethod()
    {
        StartCoroutine(StartAnim());
    }

    // Start is called before the first frame update
    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(4);
        anim.SetTrigger("14zari");
    }
}
