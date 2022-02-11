using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreen : MonoBehaviour
{  
    Camera cam;
    GameObject arrowCS;
    Canvas canvas;
    Image arrowImage;
    Vector3 middleScreen;
    RectTransform rt;

    bool isTracking = true;

    private void OnEnable()
    {
        cam = GameObject.Find("AR Camera (DO NOT CHANGE NAME)").GetComponent<Camera>();
        arrowCS = GameObject.Find("ArrowCS");
        canvas = GameObject.Find("MainCanvas(DO NOT CHANGE NAME)").GetComponent<Canvas>();

        arrowImage = arrowCS.GetComponent<Image>();
        rt = arrowCS.GetComponent<RectTransform>();

        Color c;
        if (GetComponent<Renderer>().isVisible)
        {
            c = Color.white;
            c.a = 0;
        }
        else
        {
            UpdatePositionAndRotation();
            c = Color.white;
            c.a = 1;
        }
        arrowImage.color = c;
    }

    void Update()
    {
        if (isTracking)
        {
            UpdatePositionAndRotation();
        }
    }
    private void UpdatePositionAndRotation()
    {
        var targetPosLocal = cam.transform.InverseTransformPoint(this.transform.position);              //Calculate rotation
        var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg;
        arrowImage.transform.localRotation = Quaternion.Euler(0, 0, targetAngle);

        Vector3 tmpPos = calculateWorldPosition(this.transform.position, cam);                          //Fix position - Unity bug
        arrowImage.transform.position = cam.WorldToScreenPoint(tmpPos);

        float posX = rt.rect.width * canvas.scaleFactor;
        float posY = rt.rect.height * canvas.scaleFactor;



        if (arrowImage.transform.position.x <= 0f + posX / 2)                                       //Change position to edges
        {
            arrowImage.transform.position = new Vector3(0f + posX / 2, arrowImage.transform.position.y, arrowImage.transform.position.z);
        }
        else if (arrowImage.transform.position.x >= cam.pixelWidth - posX / 2)
        {
            arrowImage.transform.position = new Vector3(cam.pixelWidth - posX / 2, arrowImage.transform.position.y, arrowImage.transform.position.z);
        }

        if (arrowImage.transform.position.y <= 0f + posY / 2)
        {
            arrowImage.transform.position = new Vector3(arrowImage.transform.position.x, 0f + posY / 2, arrowImage.transform.position.z);
        }
        else if (arrowImage.transform.position.y >= cam.pixelHeight - posY / 2)
        {
            arrowImage.transform.position = new Vector3(arrowImage.transform.position.x, cam.pixelHeight - posY / 2, arrowImage.transform.position.z);
        }
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
        Color c = Color.white;
        c.a = 0;
        arrowImage.color = c;
        isTracking = false;
    }
    private void OnBecameInvisible()
    {
        Color c = Color.white;
        c.a = 1;
        arrowImage.color = c;
        isTracking = true;
    }
    private void OnDisable()
    {
        try
        {
            Color c = Color.white;
            c.a = 0;
            arrowImage.color = c;
        }
        catch { }
        
    }
   

}
