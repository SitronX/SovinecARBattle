using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zari17 : MonoBehaviour,Zari
{
    // Start is called before the first frame update
    public Animator attackingAnimator;
    public Animator defendingAnimator;
    public Animator defaultAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void StartMethod()
    {
        attackingAnimator.SetTrigger("17zari");
        defendingAnimator.SetTrigger("17zari");
        defaultAnimator.SetTrigger("17zari");
    }

 
   
}
