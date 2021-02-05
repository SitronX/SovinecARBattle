using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zari18 : MonoBehaviour,Zari
{
    // Start is called before the first frame update
    public Animator animator;
    public Animator attackingAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartMethod()
    {
        animator.SetTrigger("Start");
        attackingAnimator.SetTrigger("18zari");
    }
   

    
}
