using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Transactions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
{
    // Start is called before the first frame update
   


    public TMPro.TMP_Dropdown dd;
    public GameObject prefab;
    public GameObject gravityPrefab;

    private GameObject instance;
    private GameObject gravityCenter;
     
    private ARRaycastManager raycastM;
    private Vector2 touchPos;    

    public static bool planesEnabled = true;


    public static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARAnchorManager anchors;
    private ARAnchor anchor;

    public Action inputDetected;
    [SerializeField] List<GameObject> uiIgnoredObjects = new List<GameObject>();

    private void OnEnable()
    {
        ChangePlanes(planesEnabled);
    }
    private void OnDisable()
    {
        Destroy(instance);
        Destroy(gravityCenter);
    }

    private void Awake()
    {
        raycastM = GetComponent<ARRaycastManager>();
        anchors = this.GetComponent<ARAnchorManager>();

        
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
    

    void Update()
    {
        if ((TryGetTouchPosition(out Vector2 touchPos)==0))
        { 
            return; 
        }
        else if (IsPointerOverUIObject())
        {
            if (uiIgnoredObjects.Find(i => i.activeSelf == true)) return;
            
            inputDetected?.Invoke();
            
        }              
        else if(TryGetTouchPosition(out Vector2 touchPos1) == 1)
        {
            inputDetected?.Invoke();
            raycastM.Raycast(touchPos1, hits, TrackableType.PlaneWithinPolygon);
            var hitpose = hits[0].pose;
            Vector3 pos = hitpose.position;
            if(instance==null)
            {
                instance = Instantiate(prefab, pos, Quaternion.identity);           
                this.GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, hitpose.position, Quaternion.identity);
            }
            else
            {
                instance.transform.position = hitpose.position;
            }
        }
        else
        {
            inputDetected?.Invoke();
        }

    }
    public static void ChangePlanes(bool val)
    {
        
        planesEnabled = val;

        ARPlaneManager tmp = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();       
        foreach (var i in tmp.trackables)                                                               
        {                                                                                               
            i.gameObject.SetActive(val);
        }
        tmp.enabled = val;            
      
    }


    private bool IsPointerOverUIObject()            //Code borrowed from: https://answers.unity.com/questions/1115464/ispointerovergameobject-not-working-with-touch-inp.html 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
