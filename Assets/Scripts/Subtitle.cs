using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro txt;
    public Camera cam;
    void Start()
    {
        cam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.transform.LookAt(cam.transform);
    }

    public void SubtitleTimes()
    {
        StartCoroutine(ChangeText("1643", 0));
        StartCoroutine(ChangeFontSize(12, 5));
        StartCoroutine(ChangeText("16.9.1643-7.10.1643 došlo k bitvì se švédskými vojsky", 5));
        StartCoroutine(ChangeText("Obrana hradu nevydržela nápor útoèících vojsk a hrad byl dobyt", 11));
        StartCoroutine(ChangeText("Až do roku 1650 švédská vojska tento hrad držela", 17));


    }

    IEnumerator ChangeText(string text,int time)
    {
        yield return new WaitForSeconds(time);
        txt.text = text;
    }
    IEnumerator ChangeFontSize(int size, int time)
    {
        yield return new WaitForSeconds(time);
        txt.fontSize = size;
    }
}
