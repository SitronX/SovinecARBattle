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


    public static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARAnchorManager anchors;
    private ARAnchor anchor;

    public Action inputDetected;

    private void OnEnable()
    {
        planesEnabled = false;
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
        if((TryGetTouchPosition(out Vector2 touchPos)==0))
        { 
            return; 
        }
        else if(IsPointerOverUIObject()y)
        {
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

                //anchor = anchors.AddAnchor(new Pose(hitpose.position, hitpose.rotation));     
                //instance.transform.parent = anchor.transform;

                //gravityCenter = Instantiate(gravityPrefab, pos, hitpose.rotation);
                //gravityCenter.transform.Translate(new Vector3(0, -9.81f, 0), Space.Self);
                //Physics.gravity = gravityCenter.transform.position;  //Quaternion.identity je v ARFoundation divne naklonen, musi se proto pouzit rotace z raycastu, nebo z rozpoznavaneho obrazku. 
                                                                     //Pote se obrazek polozi do spravne rotace, ale physics.gravity funguje porad s (0,-9.81f,0), kvuli tomu ze je zde vyuzita primitivni fyzika k delovym koulim a kamenum, musi se upravit s pomoci metody vyse se vytahne korektni vector 3 hodnota a ta se priradi

                this.GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, hitpose.position, Quaternion.identity);
            }
            else
            {
                //gravityCenter.transform.position = hitpose.position;
                //gravityCenter.transform.Translate(new Vector3(0, -9.81f, 0), Space.Self);
                //Physics.gravity = gravityCenter.transform.position;

                instance.transform.position = hitpose.position;
                //this.GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, hitpose.position, Quaternion.identity);

            }

        }
        else
        {
            inputDetected?.Invoke();
            if(instance!=null)
            {

                this.gameObject.transform.Rotate(new Vector3(0, 2, 0), Space.Self);
                
            }         
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

    //DELETE IF EVERYTHING WORKS FINE

    private bool IsPointerOverUIObject()            //Code borrowed from: https://answers.unity.com/questions/1115464/ispointerovergameobject-not-working-with-touch-inp.html 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
