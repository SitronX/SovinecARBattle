using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    private Animator anim;
    private TextMeshProUGUI canvasSubtitle;

    public List<string> subtitles;
    public List<int> timing;
    public AudioClip currentAudio;
    public float currentAudioDelay;

    public static List<Subtitle> allSubtitles = new List<Subtitle>();
    public static List<AudioSource> allAudios = new List<AudioSource>();
    void Start()
    {
        cam = GameObject.Find("AR Camera").GetComponent<Camera>();

        GameObject tmp = GameObject.Find("CanvasSubtitles");
        canvasSubtitle = tmp.GetComponent<TextMeshProUGUI>();
        anim = tmp.GetComponent<Animator>();

        allSubtitles.Add(this);

    }

    // Update is called once per frame


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

        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();

        try
        {
            audioSource.clip = currentAudio;
        }
        catch
        {

        }
        audioSource.Play();
        allAudios.Add(audioSource);
    }

    IEnumerator ChangeText(string text,int time)
    {
        yield return new WaitForSeconds(time);

        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1);

        canvasSubtitle.text = text;
    }
    IEnumerator ChangeFontSize(int size, int time)
    {
        yield return new WaitForSeconds(time);
        canvasSubtitle.fontSize = size;
    }
}
