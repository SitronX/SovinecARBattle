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

    public static GameObject instance;
    private GameObject gravityCenter;
     
    private ARRaycastManager raycastM;
    private Vector2 touchPos;    

    public static bool planesEnabled = false;
    public static bool helpPlaneStatus = true;                 //Just for ui collapsing, so we know which state we were in previously


    public static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public static Action<GameObject> objectPlaced;

    public Action inputDetected;
    public Action backButtonDetected;
    [SerializeField] List<GameObject> uiIgnoredObjects = new List<GameObject>();
    [SerializeField] ARSession m_Session;
    [SerializeField] Camera cam;
    [SerializeField] GameObject placedPlane;
    [SerializeField] Animator arPopupAnim;

    Vector3 zOffset = new Vector3(0, 0, 0);
    [SerializeField] Slider testSlider;
    Vector3 latestHit = new Vector3(0, 0, 0);
    Vector3 withoutARpos = new Vector3(0, 0, 0);
    Vector3 withoutARrot = new Vector3(0, 23, 0);
    Vector3 withoutARsessionPos = new Vector3(0.5f, 1.5f, -2);
    Vector3 withoutARsessionRot = new Vector3(40, 0, 0);
    bool isModelVisible = false;

    public static bool UsingAR { get; set; }
    private Vector2 lastTouch = Vector2.zero;



    private void OnEnable()
    {
        ChangePlanes(false,false);                      //Ugly but works
        helpPlaneStatus = true;
        ModelVisibility.modelVisible += ModelVisibilityChange;
        StartCoroutine(DetermineWhatTypeToLaunch());

    }
    private void OnDisable()
    {
        Destroy(instance);
        Destroy(gravityCenter);
    }

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
    

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))                //Back button pressed
        {
            backButtonDetected?.Invoke();
        }
        if ((TryGetTouchPosition(out Vector2 touchPos)==0))         //Touch logic start
        {
            if (lastTouch != Vector2.zero) lastTouch = Vector2.zero;
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

            if (instance==null)
            {
                if(UsingAR)                                 //SPAWN IF USING AR
                {
                    if (!RayCasting(true, touchPos1)) return;

                    instance = Instantiate(prefab, latestHit, Quaternion.identity);
                    GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, latestHit);
                    objectPlaced?.Invoke(instance);
                }
                else
                {                                                                   //SPAWN IF NOT USING AR
                    if (!RayCasting(false, touchPos1)) return;

                    instance = Instantiate(prefab, latestHit, Quaternion.identity);
                    objectPlaced?.Invoke(instance);

                }


            }
            else
            {
               
                if(!isModelVisible)
                {
                    if (UsingAR)
                    {
                        if (!RayCasting(true, touchPos1)) return;

                        instance.transform.position = latestHit;
                        GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, latestHit);
                    }
                    else
                    {
                        if (!RayCasting(false, touchPos1)) return;

                        instance.transform.position = latestHit;

                    }


                }
                else
                {
                    //DRAG INFO
                    if (lastTouch == Vector2.zero)
                    {
                        lastTouch = touchPos1;
                    }
                    else
                    {
                        float multiplier = 0.001f;
                        Vector3 dir = touchPos1 - lastTouch;
                        Vector3 dir2 = new Vector3(dir.x * multiplier, dir.z * multiplier, dir.y * multiplier);

                        var forward = cam.transform.forward;
                        var right = cam.transform.right;

                        forward.y = 0f;
                        right.y = 0f;

                        Vector3 desiredMoveDirection = forward * dir2.z + right * dir2.x;

                        instance.transform.Translate(desiredMoveDirection);
                        lastTouch = touchPos1;

                        if(UsingAR)
                        {
                            GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, instance.transform.position);

                        }

                    }

                }
            }
        }
        else
        {
            inputDetected?.Invoke();
            if (lastTouch != Vector2.zero) lastTouch = Vector2.zero;
        }

    }
    bool RayCasting(bool isAR,Vector2 screenPoint)
    {
        if(isAR)
        {
            if(raycastM.Raycast(screenPoint, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitpose = hits[0].pose;
                latestHit = hitpose.position + zOffset;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Ray ray = cam.ScreenPointToRay(screenPoint);
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                latestHit = hit.point+ zOffset;
                return true;
            }
            else
            {
                return false;
            }
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

    public void ChangeOffset()
    {
        zOffset = new Vector3(0,testSlider.value,0);

        instance.transform.position = latestHit + zOffset;
        this.GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, instance.transform.position);
    }


    private bool IsPointerOverUIObject()            //Code borrowed from: https://answers.unity.com/questions/1115464/ispointerovergameobject-not-working-with-touch-inp.html 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void ModelVisibilityChange(bool val)
    {
        isModelVisible = val;
    }

    public void InitializeWithoutAR()
    {
        arPopupAnim.SetBool("Hidden", true);

        Instantiate(placedPlane, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));

        instance = Instantiate(prefab, withoutARpos, Quaternion.Euler(withoutARrot));
        gameObject.transform.position = withoutARsessionPos;
        gameObject.transform.rotation = Quaternion.Euler(withoutARsessionRot);

        UsingAR = false;
        objectPlaced?.Invoke(instance);
    }

    IEnumerator  DetermineWhatTypeToLaunch()
    {
        if ((ARSession.state == ARSessionState.None)||(ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            if (instance == null)
            {
                arPopupAnim.SetBool("Hidden", false);
            }
        }
        else
        {
            // Start the AR session
            UsingAR = true;
            m_Session.enabled = true;
        }
        
    }

}
