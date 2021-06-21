using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public  bool animationsPaused = false;
    public  bool audioMuted = false;
    public Animator audioAnimator;
    public Animator pauseAnimator;
    
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
