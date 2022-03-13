using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimDay : MonoBehaviour
{
    // Start is called before the first frame update


    public string dayToLaunch;
    public Subtitle subtitleToLaunch;

    public List<Animator> animatorArrowToLaunch = new List<Animator>();
    public List<int> animatorArrowTimes = new List<int>();

    public List<Animator> animatorToLaunch = new List<Animator>();
    public List<float> animatorTimes = new List<float>();
    public List<string> animatorTriggers = new List<string>();

    [SerializeField] List<Animator> animatorBool = new List<Animator>();
    [SerializeField] List<float> animatorBoolTimes = new List<float>();
    [SerializeField] List<string> animatorBoolNames = new List<string>();
    [SerializeField] List<bool> animatorBoolValues = new List<bool>();



    public List<GameObject> objectsToEnable = new List<GameObject>();
    public List<float> objectsToEnableTimes = new List<float>();

    public List<GameObject> objectsToDisable = new List<GameObject>();
    public List<float> objectsToDisableTimes = new List<float>();


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

        foreach (GameObject i in objectsToEnable)
        {
            try
            {
                StartCoroutine(ChangeObjectState(i, objectsToEnableTimes[objectsToEnable.IndexOf(i)], true));
            }
            catch
            {
                Debug.LogError("Nelze aktivovat objekt");
            }
        }

        foreach (GameObject i in objectsToDisable)
        {
            try
            {
                StartCoroutine(ChangeObjectState(i, objectsToDisableTimes[objectsToDisable.IndexOf(i)], false));
            }
            catch
            {
                Debug.LogError("Nelze vypnout objekt");
            }
        }

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

        
        for(int i=0;i<animatorToLaunch.Count;i++)
        {
            try
            {
                StartCoroutine(StartAnimation(animatorToLaunch[i], animatorTimes[i], animatorTriggers[i]));
            }
            catch
            {
                Debug.LogError("Špatný animator, nebo jméno triggeru");
            }
        }
        for (int i = 0; i < animatorBool.Count; i++)
        {
            try
            {
                StartCoroutine(StartAnimation(animatorBool[i], animatorBoolTimes[i], animatorBoolNames[i], animatorBoolValues[i]));
            }
            catch
            {
                Debug.LogError("Špatný animator, nebo jméno triggeru");
            }
        }





        try
        {
            Animator animatorAtt = GameObject.Find("AttackingArmy").GetComponent<Animator>();
            Animator animatorDef= GameObject.Find("DefendingArmy").GetComponent<Animator>();
            StartCoroutine(StartAnimation(animatorAtt, 0, dayToLaunch));
            StartCoroutine(StartAnimation(animatorDef, 0, dayToLaunch));
        }
        catch
        {
            Debug.LogError("Armady neobsahuji urcity den");
        }

        UIGameLogic temp = GameObject.Find("UiButtons").GetComponent<UIGameLogic>();            //Kdyz byla hra pauznuta a uzivatel vybral spusteni jineho dne, UI tlacitko se zmeni a animace se spusti
        if (temp.animationsPaused)
        {
            temp.AnimationRecover();
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
    public void PauseMethod()
    {
        try
        {
            this.GetComponent<Animator>().speed = 0;
        }
        catch
        {
            Console.WriteLine("Nebyl nalezen animator");
        }



        foreach (Animator i in animatorArrowToLaunch)
        {
            try
            {
                i.speed = 0;
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
                i.speed = 0;
            }
            catch
            {
                Debug.LogError("Špatný animator, nebo jméno triggeru");
            }
        }
    }
    public void ResumeMethod()
    {
        try
        {
            this.GetComponent<Animator>().speed = 1;
        }
        catch
        {
            Console.WriteLine("Nebyl nalezen animator");
        }



        foreach (Animator i in animatorArrowToLaunch)
        {
            try
            {
                i.speed = 1;
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
                i.speed = 1;
            }
            catch
            {
                Debug.LogError("Špatný animator, nebo jméno triggeru");
            }
        }
    }

    IEnumerator StartAnimation(Animator arrowAnim,float time,string command)
    {
        yield return new WaitForSeconds(time);

        arrowAnim.SetTrigger(command);
    }
    IEnumerator StartAnimation(Animator arrowAnim, float time,string boolName, bool value)
    {
        yield return new WaitForSeconds(time);

        arrowAnim.SetBool(boolName, value);
    }

    IEnumerator ChangeObjectState(GameObject obj,float time,bool value)
    {
        yield return new WaitForSeconds(time);

        obj.SetActive(value);
    }

}
