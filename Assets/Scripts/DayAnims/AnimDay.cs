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
    public void StartMethod()
    {
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

        foreach(Animator i in animatorArrowToLaunch)
        {
            try
            {
                StartCoroutine(StartAnimation(i, animatorArrowTimes[animatorArrowToLaunch.IndexOf(i)]));
            }
            catch
            {
                Debug.LogError("Nekompatibilni animator");
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

    IEnumerator StartAnimation(Animator arrowAnim,int time)
    {
        yield return new WaitForSeconds(time);

        arrowAnim.SetTrigger("Start");
    }

}
