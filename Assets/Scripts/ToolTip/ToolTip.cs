using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI tooltipText;
    [SerializeField] GameObject bubbleSize;
    [SerializeField] UIGameLogic uiG;
    [SerializeField] RectTransform objectToRotate1;
    [SerializeField] RectTransform objectToRotate2;

    Animator tooltipAnimator;
    bool tooltipHidden = true;
    bool wasTimePaused = false;
    public static Action<Vector2> uiElementTouch;
    
    
    
    private Dictionary<int, string> tooltipValues = new Dictionary<int, string>();

    void Awake()        //Must be awake - User clicked must be launched before showtooltip
    {
        tooltipValues.Add(0, "TESTING VALUE");
        tooltipAnimator = GetComponent<Animator>();
        TapToPlace.anyInputDetected += UserClicked;
    }

    public void ShowTooltip(string text,Vector2 clickLocation,int bubbleScale)
    {
        if (clickLocation.x > (Screen.width /2))
        {
            objectToRotate1.transform.rotation=Quaternion.Euler(new Vector3(0, 180, 0));
            objectToRotate2.transform.LookAt(objectToRotate2.transform.position+new Vector3(0,0,1));
        }
        else
        {
            objectToRotate1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            objectToRotate2.transform.LookAt(objectToRotate2.transform.position + new Vector3(0, 0, 1));
        }

        bubbleSize.GetComponent<RectTransform>().offsetMax = new Vector2(bubbleScale,bubbleScale);
        transform.position = clickLocation; 
        tooltipText.text = text;
        tooltipAnimator.SetBool("Hidden", false);
        tooltipHidden = false;

        wasTimePaused = uiG.animationsPaused;
        if(!wasTimePaused)
        {
            uiG.AnimationPause();
        }
        
    }
    public void UserClicked(List<RaycastResult> res,Vector2 pos)
    {
        if(!tooltipHidden)
        {
            tooltipHidden = true;
            tooltipAnimator.SetBool("Hidden", true);
            if(!wasTimePaused)
            {
                uiG.AnimationRecover();
            }
        }

    }
}
