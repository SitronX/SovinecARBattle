using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ScrollerButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    

   
    public string dayName;
    public string objectToStartName;
    public string subtitleToStartName;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnMyButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMyButtonClick()
    {
        

        

        try
        {
            GameObject.Find(subtitleToStartName).GetComponent<Subtitle>().SubtitleTimes();
        }
        catch
        {
            Console.WriteLine("Nebyly nalezeny titulky");
        }    
        try
        {
            GameObject.Find(dayName).GetComponent<Animator>().SetTrigger("Start");
        }
        catch
        {
            Console.WriteLine("Nebyl nalezen animator");
        }

        
        try
        {
             GameObject.Find(objectToStartName).GetComponent<Zari>().StartMethod();
        }
        catch
        { 
            Console.WriteLine("Objekt neobsahuje funkci StartMethod");
        }

        

    }
}
