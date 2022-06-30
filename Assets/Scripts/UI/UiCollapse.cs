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
    [SerializeField] Animator closeAnimator;
    [SerializeField] Animator arQuestion;
    [SerializeField] TapToPlace ttp;
    [SerializeField] Button gridButton;
    [SerializeField] List<GameObject> panelsToDisable = new List<GameObject>();
    [SerializeField] UIGameLogic ugl;
    bool inputDetected = false;
    bool shrinked = false;

    public Action panelCollapsed;
    bool collapsedPan = false;
    bool collapsedClose = true;

    bool Uiclicked = false;              //Avoid rapid clicking on UI
    float clickDelay = 0.5f;


    private void OnEnable()
    {
        ttp.inputDetected += InputDetected;
        ttp.backButtonDetected += BackButtorPressed;
    }

    void Start()
    {
        StartCoroutine(CheckInput(checkEverySec));
    }

    IEnumerator CheckInput(int afterSec)
    {
        yield return new WaitForSecondsRealtime(afterSec);

        if (!inputDetected)
        {

            buttonAnimator.SetTrigger("Collapse");
            if (!panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide") && !panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide2"))
            {
                panelAnimator.SetTrigger("Spread");
            }

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
            if (panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UiPanelSpread"))
            {
                panelAnimator.SetTrigger("Shrink");
            }

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
            StartCoroutine(PreventRapidClicking(panelAnimator, val, true,true));

            inputDetected = true;
            if(TapToPlace.UsingAR) gridButton.interactable = true;
            StartCoroutine("DisablePanels");
            TapToPlace.ChangePlanes(true,true);

        }
        else if(val=="Appear")
        {
            if (!ugl.animationsPaused)
            {
                StartCoroutine(ugl.AnimationPause(0));
            }
            StartCoroutine(SpecialClosingCase());      
            StopCoroutine("DisablePanels");
        }

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
        if (TapToPlace.UsingAR) gridButton.interactable = true;
        StartCoroutine(PreventRapidClicking(panelAnimator, "Collapse",true,true));
        TapToPlace.ChangePlanes(true,true);
    }
    public void BackButtorPressed()
    {
        if (collapsedPan)
        {
            if (collapsedClose && arQuestion.GetBool("Hidden"))
            {
                StartCoroutine(PreventRapidClicking(closeAnimator, "Hidden", false, false));
         
            }
            else
            {
                StartCoroutine(PreventRapidClicking(closeAnimator, "Hidden", false, true));
                Uiclicked = false;                                                                   //Rychly hnusny hack, pri rozsireni kodu nutno refaktorizovat
                StartCoroutine(PreventRapidClicking(arQuestion, "Hidden", false, true));
            }
        }   
        else if (!panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide") && !panelAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIPanelHide2"))
        {
            SetPanelAnimator("Collapse");
        }
    }
    IEnumerator SpecialClosingCase()
    {
        if(!Uiclicked)
        {
            Uiclicked = true;

            collapsedPan = false;
            panelAnimator.SetTrigger("Appear");
            if(TapToPlace.UsingAR) gridButton.interactable = false;
            TapToPlace.ChangePlanes(false, true);

            if (!collapsedClose)                                 //If info button is pressed when having quit dialog, hide quit dialogue and open info, No delay, because this has to happen every time
            {
                collapsedClose = true;
                closeAnimator.SetBool("Hidden", collapsedClose);
                arQuestion.SetBool("Hidden", collapsedClose);
            }
            yield return new WaitForSecondsRealtime(clickDelay);

            Uiclicked = false;
        }
       
    }
    IEnumerator PreventRapidClicking(Animator animator,string trigger,bool panel,bool val)
    {
        if(!Uiclicked)
        {
            if(panel)
            {
                Uiclicked = true;
                animator.SetTrigger(trigger);
                panelCollapsed?.Invoke();
                collapsedPan = val;

                yield return new WaitForSecondsRealtime(clickDelay);
               
                Uiclicked = false;
   
            }
            else
            {
                Uiclicked = true;
                collapsedClose = val;

                animator.SetBool(trigger,collapsedClose);

                yield return new WaitForSecondsRealtime(clickDelay);
                
                Uiclicked = false;
            }
        }
    }
    public void SwitchToPanel(GameObject obj)
    {
        obj.SetActive(true);

        foreach (GameObject i in panelsToDisable)
        {
            if(i!=obj) i.SetActive(false);
        }
    }
    IEnumerator DisablePanels()
    {
        yield return new WaitForSeconds(1);

        foreach (GameObject i in panelsToDisable)
        {
            i.SetActive(false);
        }
    }
}
