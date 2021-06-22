using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelperScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public GameObject prefab;

    public TMP_Text text;
    private void Start()
    {
        obj = GameObject.Find("castle");
    }

    public void SpawnCastle()
    {
        Destroy(obj);

        Subtitle.allAudios.Clear();
        Subtitle.allSubtitles.Clear();

        obj =Instantiate(prefab, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 0)));

        ShowGravity();
    }
    
    public void ShowGravity()
    {
        try
        {
            text.text = Physics.gravity.x + "," + Physics.gravity.y + "," + Physics.gravity.z;
        }
        catch(Exception e)
        {
            text.text = e.ToString();
        }
    }

}
