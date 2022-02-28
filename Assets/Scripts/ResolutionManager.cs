using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ResolutionManager : MonoBehaviour
{
    // Start is called before the first frame update


    private void Awake()
    {
        Application.targetFrameRate = 60;
        GetComponent<ARSession>().matchFrameRateRequested = true;
    }
    public void QuitApp()
    {
        Application.Quit();
    }

}
