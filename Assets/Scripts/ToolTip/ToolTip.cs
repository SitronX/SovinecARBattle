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
