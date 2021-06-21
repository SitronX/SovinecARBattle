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
            try
            {
                AnimDay.lastAnimDay.StopAllCoroutines();
                AnimDay.lastAnimDay.StopMethod();
                foreach (AudioSource i in Subtitle.allAudios)
                {
                    i.Stop();
                }
                foreach (Subtitle i in Subtitle.allSubtitles)
                {
                    i.StopAllCoroutines();
                }
            }
            catch
            {

            }
           
        }

        if (nextDayDelay > 0 && nextButton != null)
        {
            StartCoroutine(AutomaticLaunchNextDay());
        }
        else
        {
            Console.WriteLine("Objekt nevyplněn");
        }
           
       
    }
    IEnumerator AutomaticLaunchNextDay()
    {
        

        yield return new WaitForSeconds(nextDayDelay);

        nextButton.GetComponent<Button>().Select();
        nextButton.GetComponent<ScrollerButtonClick>().OnMyButtonClick();

        

    }


}
