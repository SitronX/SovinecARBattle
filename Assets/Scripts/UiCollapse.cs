using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCollapse : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int checkEverySec = 5;
    [SerializeField] Animator buttonAnimator;
    [SerializeField] Animator panelAnimator;
    [SerializeField] TapToPlace ttp;
    [SerializeField] List<GameObject> panelsToDisable = new List<GameObject>();
    bool inputDetected = false;
    bool shrinked = false;

    public Action panelCollapsed;


    private void OnEnable()
    {
        ttp.inputDetected += InputDetected;
    }

    void Start()
    {
        StartCoroutine(CheckInput(checkEverySec));
    }

    // Update is called once per frame
    IEnumerator CheckInput(int afterSec)
    {
        yield return new WaitForSeconds(afterSec);

        if (!inputDetected)
        {

            buttonAnimator.SetTrigger("Collapse");
            panelAnimator.SetTrigger("Spread");
            shrinked = true;
        }
        else
        {
            inputDetected = false;
            StartCoroutine(CheckInput(afterSec));
        }
    }
    void InputDetected()
    {
        if(shrinked)
        {
            shrinked = false;
            buttonAnimator.SetTrigger("Appear");
            panelAnimator.SetTrigger("Shrink");
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
            if(buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("UiCollapse"))
            {
                shrinked = false;
                buttonAnimator.SetTrigger("Appear");
                StartCoroutine(CheckInput(checkEverySec));
            }

            panelCollapsed?.Invoke();
            StartCoroutine("DisablePanels");
        }
        else if(val=="Appear")
        {
            StopCoroutine("DisablePanels");
        }
        panelAnimator.SetTrigger(val);
    }
    public void DisablePanelFromButtons()
    {
        if (panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide") || panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide2")) return;
        panelAnimator.SetTrigger("Collapse");
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
