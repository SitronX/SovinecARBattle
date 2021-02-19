using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollerButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
   
    public string dayName;

    public GameObject nextButton;
    public int nextDayDelay;

    private static List<ScrollerButtonClick> allButtons = new List<ScrollerButtonClick>();

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnMyButtonClick);

        allButtons.Add(this);
    }
    

    public void OnMyButtonClick()
    {
        foreach(ScrollerButtonClick i in allButtons)
        {
            i.StopAllCoroutines();
        }
        
        try
        {
             GameObject.Find(dayName).GetComponent<AnimDay>().StartMethod();
        }
        catch
        { 
            Console.WriteLine("Objekt neobsahuje funkci StartMethod");
        }

        if (nextDayDelay > 0 && nextButton != null)
        {
            StartCoroutine(AutomaticLaunchNextDay());
        }
        else
        {
            Console.WriteLine("Objekt nevyplnìn");
        }
           
       
    }
    IEnumerator AutomaticLaunchNextDay()
    {
        

        yield return new WaitForSeconds(nextDayDelay);

        nextButton.GetComponent<Button>().Select();
        nextButton.GetComponent<ScrollerButtonClick>().OnMyButtonClick();

        

    }


}
