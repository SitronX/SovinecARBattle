using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
{
    // Start is called before the first frame update
   


    public TMPro.TMP_Dropdown dd;
    public GameObject prefab;
    private GameObject instance;
    private ARRaycastManager raycastM;
    private Vector2 touchPos;

    

    public static bool planesEnabled = true;


    public static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARAnchorManager anchors;

    private void OnEnable()
    {
        planesEnabled = false;
        ChangePlanes();
    }
    private void OnDisable()
    {
        Destroy(instance);
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
            if(instance==null)
            {

                //instance = Instantiate(prefab, pos, Quaternion.Euler(new Vector3(0,0,0)));      //hitpose.rotation
                instance = Instantiate(prefab, pos, hitpose.rotation);

                ARAnchor anchor = anchors.AddAnchor(new Pose(hitpose.position, hitpose.rotation));     
                instance.transform.parent = anchor.transform;
                Physics.gravity = new Vector3(pos.x,pos.y-9.81f,pos.z);  //Quaternion.identity je v ARFoundation divne naklonen, musi se proto pouzit rotace z raycastu, nebo z rozpoznavaneho obrazku. 
                                                                         //Pote se obrazek polozi do spravne rotace, ale physics.gravity funguje porad s (0,-9.81f,0), kvuli tomu ze je zde vyuzita primitivni fyzika k delovym koulim a kamenum, musi se upravit
            }
            else
            {
                instance.transform.position = hitpose.position;          
            }
                
        }
        else
        {
                                   //Odkomentovat k otaceni
            if(instance!=null)
            {
                instance.transform.Rotate(new Vector3(0, 2, 0), Space.Self);
                
            }
            
            
        }
        
    }

    
  
   

    public static void ChangePlanes()
    {
        if(planesEnabled)
        {
            planesEnabled = false;

            ARPlaneManager tmp = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();       
            foreach (var i in tmp.trackables)                                                               
            {                                                                                               
                i.gameObject.SetActive(false);
            }
            tmp.enabled = false;
            
        }
        else
        {
            planesEnabled = true;

            ARPlaneManager tmp = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
            tmp.enabled = true;
            foreach (var i in tmp.trackables)
            {
                i.gameObject.SetActive(true);
            }
            
        }
    }

}
