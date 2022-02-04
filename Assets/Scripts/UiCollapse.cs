using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCollapse : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int checkEverySec = 5;
    [SerializeField] Animator buttonAnimator;
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
            //buttonAnimator.SetFloat("Blend", 1);

            buttonAnimator.SetTrigger("Collapse");
            //movement = true;
            //StartCoroutine(Show());
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
            //buttonAnimator.SetFloat("Blend", -1); 
            buttonAnimator.SetTrigger("Appear");
            //StopCoroutine(Show());
            //movement = false;
           // StartCoroutine(Show());
            StartCoroutine(CheckInput(checkEverySec));
        }
        else
        {
            inputDetected = true;
        }
    }
    //IEnumerator Show()
    //{
    //
    //    if (movement)
    //    {
    //        blendValue += (0.005f/1f);
    //
    //        yield return new WaitForSeconds(0.005f);
    //
    //        if (blendValue <= 1 && blendValue >= 0)
    //        {
    //            buttonAnimator.SetFloat("Blend", blendValue);
    //            StartCoroutine(Show());
    //        }
    //        else
    //        {
    //            blendValue = Mathf.Clamp01(blendValue);
    //        }
    //    }
    //    else
    //    {
    //        blendValue -= (0.005f / 1f);
    //        yield return new WaitForSeconds(0.005f);
    //
    //        if (blendValue <= 1 && blendValue >= 0)
    //        {
    //            buttonAnimator.SetFloat("Blend", blendValue);
    //            StartCoroutine(Show());
    //        }
    //        else
    //        {
    //            blendValue = Mathf.Clamp01(blendValue);
    //        }
    //    }
    //}

}
