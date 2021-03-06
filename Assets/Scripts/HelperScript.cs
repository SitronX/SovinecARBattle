using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject obj;

    private void Start()
    {
        obj = GameObject.Find("castle");
    }

    public void Hide()
    {
        try
        {
            obj.SetActive(false);
        }
        catch
        {

        }


        
    }
    public void Show()
    {
        try
        {
            obj.SetActive(true);
        }
        catch
        {

        }
    }
}
