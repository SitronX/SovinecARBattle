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

    public static bool planesEnabled = false;
    public static bool helpPlaneStatus = true;                 //Just for ui collapsing, so we know which state we were in previously


    public static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isObjectRotating=false;

    public Action objectPlaced;

    public Action inputDetected;
    public Action backButtonDetected;
    [SerializeField] List<GameObject> uiIgnoredObjects = new List<GameObject>();

    private void OnEnable()
    {
        ChangePlanes(false,false);                      //Ugly but works
        helpPlaneStatus = true;

    }
    private void OnDisable()
    {
        Destroy(instance);
        Destroy(gravityCenter);
    }

    private void Awake()
    {
        raycastM = GetComponent<ARRaycastManager>();
        GetComponent<PinchRotate>().rotating += ChangeRotationState;

        
    }
    void ChangeRotationState(bool val)
    {
        isObjectRotating = val;
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
        if(Input.GetKey(KeyCode.Escape))                //Back button pressed
        {
            backButtonDetected?.Invoke();
        }
        if ((TryGetTouchPosition(out Vector2 touchPos)==0))         //Touch logic start
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
                this.GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, hitpose.position);
                objectPlaced?.Invoke();
            }
            else
            {
                //if (isObjectRotating) return;                   //Comment or not???

                instance.transform.position = hitpose.position;
                this.GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, hitpose.position);
            }
        }
        else
        {
            inputDetected?.Invoke();
        }

    }
    public static void ChangePlanes(bool val,bool isPanelCall)      
    {
        if(!isPanelCall)                    //When panel is active all tracking is disabled and hidden
        {
            helpPlaneStatus = val;
        }
        else
        {
            if(val)
            {
                val = helpPlaneStatus;
            }
        }
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
