using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class rijen1 : Zari
{
    // Start is called before the first frame update
    public Material transMat;
    public Material normalMat;
    public Animator animator;

    private GameObject castle;

    override
    public void StartMethod()
    {
        animator.SetTrigger("Start");

        castle = GameObject.Find("castle");
        foreach(Transform i in castle.transform)
        {
            try
            {
                i.GetComponent<MeshRenderer>().material = transMat;
                
            }
            catch { }
        }
        StartCoroutine(Cloak(1));
        StartCoroutine(UnCloak(17));
        StartCoroutine(FinishDay());



        
    }
    IEnumerator Cloak(int time)
    {

        yield return new WaitForSeconds(time);

        while(true)
        {
            if (transMat.color.a >= 0.3)
            {
                Color c = transMat.color;
                c.a -= 0.05f;
                transMat.color = c;

            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }

            

        
    }
    IEnumerator UnCloak(int time)
    {
        yield return new WaitForSeconds(time);

        while (true)
        {
            if (transMat.color.a <= 0.95)
            {
                Color c = transMat.color;
                c.a += 0.05f;
                transMat.color = c;

            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }



        
    }

    IEnumerator FinishDay()
    {
        yield return new WaitForSeconds(19);

        foreach (Transform i in castle.transform)
        {
            try
            {
                i.GetComponent<MeshRenderer>().material = normalMat;
            }
            catch { }
        }


    }


}
