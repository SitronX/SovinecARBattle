using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeepButtonHighlighted : MonoBehaviour
{
    // Start is called before the first frame update
    private EventSystem eventS;
    private GameObject lastSelected = null;

    // Update is called once per frame

    private void Start()
    {
        eventS = GetComponent<EventSystem>();
    }


    
    private void Update()           //EventSystem bohuzel neobsahuje delegata onSelectionChange nebo neco podobneho, proto takto hnusne
    {
        if (eventS.currentSelectedGameObject != null)
        {
            lastSelected = eventS.currentSelectedGameObject;
        }
        else
        {
            eventS.SetSelectedGameObject(lastSelected);
        }
    }


}
