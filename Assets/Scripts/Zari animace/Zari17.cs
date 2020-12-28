using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zari17 : Zari
{
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override
    public void StartMethod()
    {
        StartCoroutine(AnimStart());
    }
    IEnumerator AnimStart()
    {
        yield return new WaitForSeconds(27);
        animator.SetTrigger("Tower");


    }
}
