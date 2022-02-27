using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCollapse : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int checkEverySec = 5;
    [SerializeField] Animator buttonAnimator;
    [SerializeField] Animator panelAnimator;
    [SerializeField] TapToPlace ttp;
    [SerializeField] Button gridButton;
    [SerializeField] List<GameObject> panelsToDisable = new List<GameObject>();
    [SerializeField] UIGameLogic ugl;
    bool inputDetected = false;
    bool shrinked = false;

    public Action panelCollapsed;


    private void OnEnable()
    {
        ttp.inputDetected += InputDetected;
        ttp.backButtonDetected += BackButtorPressed;
    }

    void Start()
    {
        StartCoroutine(CheckInput(checkEverySec));
    }

    // Update is called once per frame
    IEnumerator CheckInput(int afterSec)
    {
        yield return new WaitForSecondsRealtime(afterSec);

        if (!inputDetected)
        {

            buttonAnimator.SetTrigger("Collapse");
            if (!panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide") && !panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide2")) panelAnimator.SetTrigger("Spread");

            shrinked = true;
        }
        else
        {
            inputDetected = false;
            StartCoroutine(CheckInput(afterSec));
        }
    }
    public void InputDetected()
    {
        if(shrinked)
        {
            shrinked = false;
            buttonAnimator.SetTrigger("Appear");
            if (panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelSpread"))   panelAnimator.SetTrigger("Shrink");


            StartCoroutine(CheckInput(checkEverySec));
        }
        else
        {
            inputDetected = true;
        }
    }
    public void SetPanelAnimator(string val)
    {
        if(val=="Collapse")
        {
            if (ugl.animationsPaused)
            {
                ugl.AnimationRecover();
            }
            if (buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("UiCollapse"))
            {
                shrinked = false;
                buttonAnimator.SetTrigger("Appear");
                StartCoroutine(CheckInput(checkEverySec));
            }
            inputDetected = true;

            gridButton.interactable = true;
            panelCollapsed?.Invoke();
            StartCoroutine("DisablePanels");
            TapToPlace.ChangePlanes(true,true);

            

        }
        else if(val=="Appear")
        {
            if (!ugl.animationsPaused)
            {
                StartCoroutine(ugl.AnimationPause(1f));
            }
            gridButton.interactable = false;
            TapToPlace.ChangePlanes(false, true);
            
            StopCoroutine("DisablePanels");
        }
        panelAnimator.SetTrigger(val);
    }
    public void DisablePanelFromButtons()
    {
        if (ugl.animationsPaused)
        {
            ugl.AnimationRecover();
        }
        if (panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide") || panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide2")) return;

        inputDetected = true;
        StartCoroutine("DisablePanels");
        gridButton.interactable = true;
        panelAnimator.SetTrigger("Collapse");
        TapToPlace.ChangePlanes(true,true);
    }
    public void BackButtorPressed()
    {
        SetPanelAnimator("Collapse");
    }

    IEnumerator DisablePanels()
    {
        //Only if prefab is placed
        yield return new WaitForSeconds(1);

        foreach(GameObject i in panelsToDisable)
        {
            i.SetActive(false);
        }
    }
}
