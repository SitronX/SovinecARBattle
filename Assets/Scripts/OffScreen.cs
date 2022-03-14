using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OffScreen : MonoBehaviour
{  
    Camera arCamera;
    Camera nonArCam;
    GameObject arrowCS;
    Canvas canvas;
    Image arrowImage;
    RectTransform rt;
    Color c = Color.white;

    Transform arrowParent;
    bool isTracking = false;

    static List<GameObject> doubleObjectList=new List<GameObject>();                //Ensure that only one attention object can be active at the same time

    private void OnEnable()
    {
        doubleObjectList.Add(this.gameObject);
        
        arCamera = GameObject.Find("AR Camera (DO NOT CHANGE NAME)").GetComponent<Camera>();
        nonArCam = GameObject.Find("NONARCAMERA (DO NOT CHANGE NAME)").GetComponent<Camera>();
        canvas = GameObject.Find("MainCanvas(DO NOT CHANGE NAME)").GetComponent<Canvas>();
        arrowParent = GameObject.Find("ArrowParent(DO NOT CHANGE NAME)").GetComponent<Transform>();
        arrowCS = arrowParent.transform.GetChild(0).gameObject;
        
        arrowImage = arrowCS.GetComponent<Image>();
        rt = arrowCS.GetComponent<RectTransform>();
        
        StartCoroutine(CheckVisibilityAfterFirstFrame());       //Check after first frame, cause object is enabled here, but renderer logic for object is still not
    }    
    IEnumerator CheckVisibilityAfterFirstFrame()        
    {
        UpdatePositionAndRotation();

        yield return new WaitForFixedUpdate();

        if (GetComponent<Renderer>().isVisible)
        {
            c.a = 0;
            isTracking = false;
        }
        else
        {
            c.a = 1;
            isTracking = true;
            StartCoroutine(UpdateArrow());
        }
        arrowImage.color = c;
    }
    IEnumerator UpdateArrow()
    {
        if (doubleObjectList.Last() != this.gameObject) yield break;

        while(isTracking)
        {
            yield return 0;                         //wait one frame
            UpdatePositionAndRotation();
        }
    }

    private void UpdatePositionAndRotation()
    {

        Camera cam;
        if(TapToPlace.UsingAR)
        {
            cam = arCamera;
        }
        else
        {
            cam = nonArCam;
        }
        //Vector3 tmpPos = calculateWorldPosition(this.transform.position, cam);                          //Fix position - Unity bug
        Vector3 tmpPos = transform.position;
        arrowParent.transform.position = cam.WorldToScreenPoint(tmpPos);

        float posX = rt.rect.width * canvas.scaleFactor;
        float posY = rt.rect.height * canvas.scaleFactor;

        if (arrowParent.transform.position.x <= 0f + posX / 2)                                       //Change position to edges
        {
            arrowParent.transform.position = new Vector3(0f + posX / 2, arrowParent.transform.position.y, arrowParent.transform.position.z);
        }
        else if (arrowParent.transform.position.x >= cam.pixelWidth - posX / 2)
        {
            arrowParent.transform.position = new Vector3(cam.pixelWidth - posX / 2, arrowParent.transform.position.y, arrowParent.transform.position.z);
        }

        if (arrowParent.transform.position.y <= 0f + posY / 2)
        {
            arrowParent.transform.position = new Vector3(arrowParent.transform.position.x, 0f + posY / 2, arrowParent.transform.position.z);
        }
        else if (arrowParent.transform.position.y >= cam.pixelHeight - posY / 2)
        {
            arrowParent.transform.position = new Vector3(arrowParent.transform.position.x, cam.pixelHeight - posY / 2, arrowParent.transform.position.z);
        }

        Vector3 targetPosLocal = cam.transform.InverseTransformPoint(transform.position);              //Calculate rotation
        float targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg;
        arrowImage.transform.localRotation = Quaternion.Euler(0, 0, targetAngle);


        

    }
    private Vector3 calculateWorldPosition(Vector3 position, Camera camera)             //Bug in unity doesnt correctly represent object position behind camera, this is for a fix from: https://forum.unity.com/threads/camera-worldtoscreenpoint-bug.85311/#post-2121212
    {
        //if the point is behind the camera then project it onto the camera plane
        Vector3 camNormal = camera.transform.forward;
        Vector3 vectorFromCam = position - camera.transform.position;
        float camNormDot = Vector3.Dot(camNormal, vectorFromCam.normalized);
        if (camNormDot <= 0f)
        {
          //we are beind the camera, project the position on the camera plane
          float camDot = Vector3.Dot(camNormal, vectorFromCam);
          Vector3 proj = (camNormal * camDot * 1.01f);   //small epsilon to keep the position infront of the camera
          position = camera.transform.position + (vectorFromCam - proj);
        }

        return position;
    }

    private void OnBecameVisible()
    {
        try
        {
            c.a = 0;
            arrowImage.color = c;
            isTracking = false;
        }
        catch { }    
    }
    private void OnBecameInvisible()
    {
        try
        {
            UpdatePositionAndRotation();
            c.a = 1;
            arrowImage.color = c;
            isTracking = true;
            StartCoroutine(UpdateArrow());

        }
        catch { }     
    }
    private void OnDisable()
    {
        doubleObjectList.Remove(this.gameObject);
        c.a = 0;
        arrowImage.color = c;      
    }
   

}
