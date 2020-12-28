using System.Collections;
using System.Collections.Generic;
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

        StartCoroutine(FinishDay());



        
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
