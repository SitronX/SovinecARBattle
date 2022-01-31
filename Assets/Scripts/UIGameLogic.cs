using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public  bool animationsPaused = false;
    public  bool audioMuted = false;
    public  bool gridActive = true;
    public Animator audioAnimator;
    public Animator pauseAnimator;
    [SerializeField] private Button gridButton;
    public Animator gridAnimator;

    [SerializeField] private DropDownSwitch dds;

    private void OnEnable()
    {
        dds.planeDetection += PlaneChange;
    }

    public void AudioStateChange()
    {
        if(!audioMuted)
        {
            audioMuted = true;
            audioAnimator.SetTrigger("SwitchState");
            AnimDay.lastAnimDay.subtitleToLaunch.currentAudioSource.volume=0;

        }
        else
        {
            audioMuted = false;
            audioAnimator.SetTrigger("SwitchState");
            AnimDay.lastAnimDay.subtitleToLaunch.currentAudioSource.volume = 1;
        }
    }
    public void AnimationStateChange()
    {
        if (!animationsPaused)
        {
            animationsPaused = true;
            StartCoroutine(AnimationPause());
        }
        else
        {
            animationsPaused = false;

            pauseAnimator.SetTrigger("SwitchState");
            AnimDay.lastAnimDay.subtitleToLaunch.currentAudioSource.Play();
            Time.timeScale = 1;
        }
    }
    public void PlaneChange(bool val)
    {
        if(TapToPlace.planesEnabled)
        {
            gridButton.interactable = true;
        }
        else
        {
            gridButton.interactable = false;
        }  
    }
    public void HidePlanes()
    {
        TapToPlace.planesEnabled = !TapToPlace.planesEnabled;

        if(TapToPlace.planesEnabled)
        {
            gridAnimator.SetTrigger("GridOn");
        }
        else
        {
            gridAnimator.SetTrigger("GridOff");

        }
    }
    public void AnimationRecover()
    {
        animationsPaused = false;

        pauseAnimator.SetTrigger("SwitchState");
        Time.timeScale = 1;
    }
    IEnumerator AnimationPause()
    {
        pauseAnimator.SetTrigger("SwitchState");
        yield return new WaitForSeconds(0.5f);
        
       
        AnimDay.lastAnimDay.subtitleToLaunch.currentAudioSource.Pause();
        Time.timeScale = 0;
    }
    

}
