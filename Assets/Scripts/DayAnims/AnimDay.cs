using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDay : MonoBehaviour
{
    // Start is called before the first frame update


    public string dayToLaunch;
    public Subtitle subtitleToLaunch;

    public List<Animator> animatorArrowToLaunch = new List<Animator>();
    public List<int> animatorArrowTimes = new List<int>();

    public List<Animator> animatorToLaunch = new List<Animator>();
    public List<int> animatorTimes = new List<int>();
    public List<string> animatorTriggers = new List<string>();

    public static AnimDay lastAnimDay = null;


    public void StartMethod()
    {

        if(lastAnimDay!=null)
        {
            lastAnimDay.StopAllCoroutines();
            lastAnimDay.StopMethod();
            lastAnimDay = this;

            StartCoroutine(Launching());
        }
        else
        {
            lastAnimDay = this;

            StartCoroutine(Launching());
        }
            


    }

    IEnumerator Launching()
    {
       

        yield return null;              //Pockani na dalsi snimek, aby mely animatory cas reagovat (Vsechny probihajici animace z minula se vypnou, pak jeden snimek na odpocinek a pak se zapnou nove animace)

        try
        {
            this.GetComponent<Animator>().SetTrigger("Start");
        }
        catch
        {
            Console.WriteLine("Nebyl nalezen animator");
        }

        try
        {
            subtitleToLaunch.SubtitleTimes();
        }
        catch
        {
            Debug.LogWarning("Nebyly nalezeny titulky");
        }

        foreach (Animator i in animatorArrowToLaunch)
        {
            try
            {
                StartCoroutine(StartAnimation(i, animatorArrowTimes[animatorArrowToLaunch.IndexOf(i)], "Start"));
            }
            catch
            {
                Debug.LogError("Nekompatibilni animator");
            }
        }

        foreach (Animator i in animatorToLaunch)
        {
            try
            {
                StartCoroutine(StartAnimation(i, animatorTimes[animatorToLaunch.IndexOf(i)], animatorTriggers[animatorToLaunch.IndexOf(i)]));
            }
            catch
            {
                Debug.LogError("Špatný animator, nebo jméno triggeru");
            }
        }

        try
        {
            GameObject.Find("AttackingArmy").GetComponent<Animator>().SetTrigger(dayToLaunch);
            GameObject.Find("DefendingArmy").GetComponent<Animator>().SetTrigger(dayToLaunch);
        }
        catch
        {
            Debug.LogError("Armady neobsahuji urcity den");
        }
    }

    public void StopMethod()
    {
        try
        {
            this.GetComponent<Animator>().SetTrigger("Stop");
        }
        catch
        {
            Console.WriteLine("Nebyl nalezen animator");
        }

        

        foreach (Animator i in animatorArrowToLaunch)
        {
            try
            {
                i.SetTrigger("Stop");
            }
            catch
            {
                Debug.LogError("Nekompatibilni animator");
            }
        }

        foreach (Animator i in animatorToLaunch)
        {
            try
            {
                i.SetTrigger("Stop");
            }
            catch
            {
                Debug.LogError("Špatný animator, nebo jméno triggeru");
            }
        }

        

      
    }

    IEnumerator StartAnimation(Animator arrowAnim,int time,string command)
    {
        yield return new WaitForSeconds(time);

        arrowAnim.SetTrigger(command);
    }

}
