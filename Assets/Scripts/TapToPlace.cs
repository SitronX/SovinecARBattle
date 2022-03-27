using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Transactions;
using TMPro;
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
    private GameObject planeInstance;
    private GameObject gravityCenter;
     
    private ARRaycastManager raycastM;
    private Vector2 touchPos;    

    public static bool planesEnabled = false;
    public static bool helpPlaneStatus = true;                 //Just for ui collapsing, so we know which state we were in previously


    public static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public static Action<List<RaycastResult>, Vector2> anyInputDetected;        //FOR tooltip
    public static Action<GameObject> objectPlaced;
    public static Action<Vector2> uiInputDetected;
    public Action inputDetected;
    public Action backButtonDetected;
    [SerializeField] List<GameObject> uiIgnoredObjects = new List<GameObject>();
    [SerializeField] ARSession m_Session;
    [SerializeField] Camera cam;
    [SerializeField] GameObject placedPlane;
    [SerializeField] Animator arPopupAnim;
    [SerializeField] Animator arSwitchAnim;
    [SerializeField] Animator arWarning;
    [SerializeField] TextMeshProUGUI arSwitchText;
    [SerializeField] TextMeshProUGUI arSwitchButtonText;

    [SerializeField] Button arIcon;
    [SerializeField] Button gridButton;
    [SerializeField] Animator arIconAnimator;

    Vector3 latestHit = new Vector3(0, 0, 0);
    Vector3 withoutARrot = new Vector3(0, 23, 0);
    Vector3 withoutARsessionPos = new Vector3(0.5f, 2.75f, -3.5f);
    Vector3 withoutARsessionRot = new Vector3(40, 0, 0);
    bool isModelVisible = false;

    public static bool UsingAR { get; set; }
    private Vector2 lastTouch = Vector2.zero;

    private Touch click1;
    private Touch click2;
    private bool doubleTapped = false;
    private float lastClickTime = 0;
    private float lastClickInterval = 0.2f;
    [SerializeField] Camera nonARCamera;



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
            //userInteraction?.Invoke(touchPos);
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
            if (doubleTapped) doubleTapped = false;
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
            click1 = Input.GetTouch(0);
            
            if(!doubleTapped)
            {
                if (lastClickTime + lastClickInterval > Time.unscaledTime)
                {
                    doubleTapped = true;
                }
            }

            if (doubleTapped)
            {
                if (instance==null)
                {
                    if (UsingAR)                                 //SPAWN IF USING AR
                    {
                        if (!RayCasting(true, touchPos1)) return;

                        instance = Instantiate(prefab, latestHit, Quaternion.identity);
                        GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, latestHit);
                        objectPlaced?.Invoke(instance);
                    }
                }
                else
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
                return;
            }
            if (instance==null)
            {
                if(UsingAR)                                 //SPAWN IF USING AR
                {
                    if (!RayCasting(true, touchPos1)) return;

                    instance = Instantiate(prefab, latestHit, Quaternion.identity);
                    GetComponent<ARSessionOrigin>().MakeContentAppearAt(instance.transform, latestHit);
                    objectPlaced?.Invoke(instance);
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
                    float multiplier = 0.002f;
                    float arMultiplier = 0.002f;

                    if (UsingAR) multiplier = arMultiplier;

                    Vector3 dir = touchPos1 - lastTouch;
                    Vector3 dir2 = new Vector3(dir.x * multiplier, dir.z * multiplier, dir.y * multiplier);

                    Vector3 forward;
                    Vector3 right;

                    if(UsingAR)
                    {
                        forward= cam.transform.forward;
                        right= cam.transform.right;
                    }
                    else
                    {
                        forward=nonARCamera.transform.forward;
                        right = nonARCamera.transform.right;
                    }

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
            if (click1.phase == TouchPhase.Ended) lastClickTime = Time.unscaledTime;        //Double tap

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
                latestHit = hitpose.position;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Ray ray = nonARCamera.ScreenPointToRay(screenPoint);
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                latestHit = hit.point;
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

    private bool IsPointerOverUIObject()            //Code borrowed from: https://answers.unity.com/questions/1115464/ispointerovergameobject-not-working-with-touch-inp.html 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        anyInputDetected?.Invoke(results, eventDataCurrentPosition.position);
        return results.Count > 0;
    }

    private void ModelVisibilityChange(bool val)
    {
        isModelVisible = val;
    }
    public void ARbuttonClicked()
    {
        if(arIconAnimator.GetBool("ArOn"))
        {
            //Subtitle.ResetAudioAndSubtitles();
            //GameObject.Find("EventSystem").GetComponent<KeepButtonHighlighted>().MakeSelectionChange(null);     
            InitializeWithoutAR();
        }
        else
        {
            Subtitle.ResetAudioAndSubtitles();
            GameObject.Find("EventSystem").GetComponent<KeepButtonHighlighted>().MakeSelectionChange(null);     
            InitializeWithAR();
        }
    }
    public void InitializeWithAR()
    {
        if (instance != null) Destroy(instance,0);
        if (planeInstance != null) Destroy(planeInstance,0);


        EnableAR();

        GetComponent<PinchRotate>().enabled = true;
        nonARCamera.GetComponent<PinchRotate>().enabled = false;
        cam.enabled = true;
        nonARCamera.enabled = false;

        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        gameObject.transform.localScale = new Vector3(3, 3, 3);

        cam.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        cam.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        cam.gameObject.transform.localScale = new Vector3(1, 1, 1);

        arIconAnimator.SetBool("ArOn", true);

    }


    public void InitializeWithoutAR()       
    {
        DisableAR();

        
        GetComponent<PinchRotate>().enabled = false;
        nonARCamera.GetComponent<PinchRotate>().enabled = true;
        cam.enabled = false;
        nonARCamera.enabled = true;

        gameObject.transform.localPosition = withoutARsessionPos;
        gameObject.transform.localRotation = Quaternion.Euler(withoutARsessionRot);
        gameObject.transform.localScale = new Vector3(3, 3, 3);

        nonARCamera.gameObject.transform.localPosition = withoutARsessionPos;
        nonARCamera.gameObject.transform.localRotation = Quaternion.Euler(withoutARsessionRot);
        nonARCamera.gameObject.transform.localScale = new Vector3(1, 1, 1);

        arPopupAnim.SetBool("Hidden", true);


        if(planeInstance==null)
        {
            planeInstance = Instantiate(placedPlane, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        }

        if(instance==null)
        {
            instance = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.Euler(withoutARrot));
        }
        else
        {
            instance.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(withoutARrot));
        }

        arIconAnimator.SetBool("ArOn", false);

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
                arIcon.interactable = false;

            }
        }
        else
        {
            // Start the AR session
            InitializeWithAR();

            arWarning.SetBool("Hidden", false);
            arIcon.interactable = true;
        }
        
    }
    private void EnableAR()
    {
        cam.gameObject.GetComponent<ARPoseDriver>().enabled = true;
        cam.gameObject.GetComponent<ARCameraManager>().enabled = true;
        cam.gameObject.GetComponent<ARCameraBackground>().enabled = true;

        ChangePlanes(true, true);       ////SImilar to panel behaviou so used again


        GetComponent<ARSessionOrigin>().enabled = true;
        GetComponent<ARRaycastManager>().enabled = true;

        m_Session.enabled = true;

        m_Session.Reset();




        UsingAR = true;

        Application.targetFrameRate = 30;
        gridButton.interactable = true;

    }
    private void DisableAR()
    {
        cam.gameObject.GetComponent<ARPoseDriver>().enabled = false;
        cam.gameObject.GetComponent<ARCameraManager>().enabled = false;
        cam.gameObject.GetComponent<ARCameraBackground>().enabled = false;

        ChangePlanes(false, true);          //SImilar to panel behaviou so used again

        GetComponent<ARSessionOrigin>().enabled = false;
        GetComponent<ARRaycastManager>().enabled = false;

        m_Session.enabled = false;

        m_Session.Reset();


        UsingAR = false;

        Application.targetFrameRate = 60;
        gridButton.interactable = false;
    }
    public void ShowSwitchPopup()
    {
        if(UsingAR)
        {
            arSwitchText.text = "Vypnout rozšířenou realitu?";
            arSwitchButtonText.text = "Vypnout";
        }
        else
        {
            arSwitchText.text = "Zapnout rozšířenou realitu?";
            arSwitchButtonText.text = "Zapnout";
        }
        arSwitchAnim.SetBool("Hidden", false);
    }
    public void HideSwitchPopup()
    {
        arSwitchAnim.SetBool("Hidden", true);
    }
    public void HideArWarning()
    {
        arWarning.SetBool("Hidden", true);
    }

}
