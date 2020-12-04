using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sl;
    public GameObject balista;

    public GameObject musketa;

    public GameObject ctenar;

    public GameObject modelHradu;
    public GameObject scrollerBackground;

    public Dropdown dd;
    public GameObject prefab;
    private GameObject instance;
    private ARRaycastManager raycastM;
    private Vector2 touchPos;

    public static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastM = GetComponent<ARRaycastManager>(); 
    }
    int TryGetTouchPosition(out Vector2 touchPos)
    {
        if(Input.touchCount==1)
        {
            touchPos = Input.GetTouch(0).position;
            return 1;
        }
        if(Input.touchCount>1)
        {
            touchPos = Input.GetTouch(0).position;
            return 2;
        }
        touchPos = default; 
        return 0;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(TryGetTouchPosition(out Vector2 touchPos)==0) 
        { 
            return; 
        }
        else if(TryGetTouchPosition(out Vector2 touchPos1) == 1)
        {
            raycastM.Raycast(touchPos1, hits, TrackableType.PlaneWithinPolygon);
            var hitpose = hits[0].pose;
            Vector3 pos = hitpose.position;
            pos.y = pos.y - 0.6f;
            if(instance==null)
            {
                Vector3 rotation = FindObjectOfType<Camera>().transform.forward;
                rotation.y += 180;
                instance = Instantiate(prefab, pos, Quaternion.Euler(rotation));
            }
            else
            {
                instance.transform.position = hitpose.position;
            }
                
        }
        else
        {
            if(instance!=null)
            {
                Vector3 rotation = instance.transform.rotation.eulerAngles;
                rotation.y += 5;

                instance.transform.rotation = Quaternion.Euler(rotation);
            }
            
            
        }
        
    }
    public void Shot()
    {
        try
        {
            instance.GetComponent<Animator>().SetTrigger("trigger");

        }
        catch { }
    }
    public void DropDownChange()
    {
        scrollerBackground.SetActive(false);

        if (dd.captionText.text=="Balista")
        {
            prefab = balista;
            Destroy(instance);
        }
        else if(dd.captionText.text=="Mušketa")
        {
            prefab = musketa;
            Destroy(instance);
        }
        else if(dd.captionText.text=="Čtenář")
        {
            prefab = ctenar;
            Destroy(instance);
        }
        else if(dd.captionText.text=="Model Historie")
        {
            prefab = modelHradu;
            Destroy(instance);
            scrollerBackground.SetActive(true);

        }
    }
    public void SliderChange()
    {
        float val = sl.value / 100 * 0.05f;
        
        GetComponent<ImageTracking>().spawnedPrefabs["fire"].transform.localScale = new Vector3(val+0.01f, val, val);
    }

}
