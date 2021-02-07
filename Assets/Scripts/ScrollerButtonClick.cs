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

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnMyButtonClick);
    }

    public void OnMyButtonClick()
    {
   
        try
        {
             GameObject.Find(dayName).GetComponent<AnimDay>().StartMethod();
        }
        catch
        { 
            Console.WriteLine("Objekt neobsahuje funkci StartMethod");
        }

        

    }
}
