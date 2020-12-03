using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Button button;

    void Start()
    {
        button.onClick.AddListener(OnMyButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMyButtonClick()
    {
        GameObject model = GameObject.Find("Model1");
        if (model == null) return;

        model.GetComponent<Anim1643>().MainAnimStart();

    }
}
