using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ScrollerButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Button button;

    public Subtitle subtitleToEnable;
    public Animator animationToPlay;

    public GameObject objectToStart;        //Script with method named method "StartMethod" for added script logic

    void Start()
    {
        button.onClick.AddListener(OnMyButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMyButtonClick()
    {
        

        try
        {
            subtitleToEnable.SubtitleTimes();
        }
        catch { Console.WriteLine("Nebyly nalezeny titulky"); }
        try
        {
            animationToPlay.SetTrigger("Start");
        }
        catch
        {
            Console.WriteLine("Nebyl nalezen animator");
        }

        
        try
        {
            objectToStart.GetComponent<Zari>().StartMethod();
        }
        catch
        { 
            Console.WriteLine("Objekt neobsahuje funkci StartMethod");
        }

        

    }
}
