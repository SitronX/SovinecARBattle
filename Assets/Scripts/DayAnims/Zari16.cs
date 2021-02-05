using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zari16 : MonoBehaviour,Zari
{
    public Animator anim;
    public void StartMethod()
    {
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(6);
        anim.SetTrigger("16zari");
    }

    // Start is called before the first frame update
 

}
