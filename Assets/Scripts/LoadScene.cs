using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider loadingSlider;

    public TextMeshProUGUI text;

    private float loadProgress=0;
    private float lastLoadProgress=0;
    void Start()
    {
        StartCoroutine(LoadingScene());
    }
    
    IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(0.1f);

        AsyncOperation loadingLevel = SceneManager.LoadSceneAsync(1);

        while(!loadingLevel.isDone)
        {
            loadProgress= Mathf.Clamp01(loadingLevel.progress/0.9f);
            
            if(lastLoadProgress!=loadProgress)
            {
                loadingSlider.value = loadProgress;

                text.text =(loadProgress*100).ToString("0.00")+ " %";

                lastLoadProgress = loadProgress;
            }            
            yield return null;
        }      
    }    
}
