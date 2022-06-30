using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private TextMeshProUGUI canvasSubtitle;

    public List<string> subtitles;
    public List<int> timing;
    public AudioClip currentAudioClip;
    public float currentAudioDelay;

    [HideInInspector]
    public AudioSource currentAudioSource;
    

    public static List<Subtitle> allSubtitles = new List<Subtitle>();
    public static List<AudioSource> allAudios = new List<AudioSource>();
    void Start()
    {
        GameObject tmp = GameObject.Find("CanvasSubtitles");
        canvasSubtitle = tmp.GetComponent<TextMeshProUGUI>();
        anim = tmp.GetComponent<Animator>();

        allSubtitles.Add(this);
    }
    public void SubtitleTimes()
    {
        foreach(Subtitle i in allSubtitles)
        {
            i.StopAllCoroutines();
        }
        foreach(AudioSource i in allAudios)
        {
            i.Stop();
        }
        for(int i=0;i<subtitles.Count;i++)
        {
            StartCoroutine(ChangeText(subtitles[i],timing[i]));

        }
        StartCoroutine(PlayAudio(currentAudioDelay));
    }
    IEnumerator PlayAudio(float delay)
    {
        yield return new WaitForSeconds(delay);

        currentAudioSource = this.gameObject.AddComponent<AudioSource>();

        try
        {
            currentAudioSource.clip = currentAudioClip;
        }
        catch
        {

        }
        UIGameLogic temp = GameObject.Find("UiButtons").GetComponent<UIGameLogic>();            //If game is muted
        if (temp.audioMuted)
        {
            currentAudioSource.volume = 0;
        }
        currentAudioSource.Play();
        allAudios.Add(currentAudioSource);
    }

    IEnumerator ChangeText(string text,int time)
    {
        yield return new WaitForSeconds(time);

        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1);

        canvasSubtitle.text = text;
    }
    public static void ResetAudioAndSubtitles()
    {
        GameObject.Find("CanvasSubtitles").GetComponent<TextMeshProUGUI>().text = "";
        foreach (Subtitle i in allSubtitles)
        {
            i.StopAllCoroutines();
        }
        foreach (AudioSource i in allAudios)
        {
            i.Stop();
        }
        allAudios.Clear();
        allSubtitles.Clear();
    }
    
}
