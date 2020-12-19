using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
    // Start is called before the first frame update
     TextMeshPro txt;
     Camera cam;
    private Animator anim;

    public List<string> subtitles;
    public List<int> timing;
    void Start()
    {
        cam = GameObject.Find("AR Camera").GetComponent<Camera>();
        txt = GetComponent<TextMeshPro>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        txt.transform.rotation = Quaternion.LookRotation(txt.transform.position-cam.transform.position);
    }

    public void SubtitleTimes()
    {
        StopAllCoroutines();

        for(int i=0;i<subtitles.Count;i++)
        {
            StartCoroutine(ChangeText(subtitles[i],timing[i]));

        }


        

    }

    IEnumerator ChangeText(string text,int time)
    {
        yield return new WaitForSeconds(time);

        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1);

        txt.text = text;
    }
    IEnumerator ChangeFontSize(int size, int time)
    {
        yield return new WaitForSeconds(time);
        txt.fontSize = size;
    }
}
