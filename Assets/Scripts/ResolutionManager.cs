using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    // Start is called before the first frame update


    private void Awake()
    {
        Resolution res = Screen.currentResolution;

        
        Screen.SetResolution(res.width/2, res.height/2, true);
    }

}
