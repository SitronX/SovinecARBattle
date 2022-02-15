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
    bool inputDetected = false;
    bool collapsed = false;
    bool movement = false;
    float blendValue = 0;

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
            panelAnimator.SetTrigger("Collapse");
            collapsed = true;
        }
        else
        {
            inputDetected = false;
            StartCoroutine(CheckInput(afterSec));
        }
    }
    void InputDetected()
    {
        if(collapsed)
        {
            collapsed = false;
            buttonAnimator.SetTrigger("Appear");
            panelAnimator.SetTrigger("Appear");
            StartCoroutine(CheckInput(checkEverySec));
        }
        else
        {
            inputDetected = true;
        }
    }
}
