using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowGUI : MonoBehaviour
{
    // Start is called before the first frame update            //SCRIPT LOGIC BORROWED FROM: https://answers.unity.com/questions/64509/get-a-gui-2d-arrow-to-turn-to-a-specific-position.html?childToView=64664#answer-64664
    [SerializeField] Camera cam;
    [SerializeField] Image arrImg;
    [SerializeField] Transform arrowWS;
     Vector3 arrowCS;
     Vector3 middleScreen;


    bool track = false;
    
    
    
    void Start()
    {

    }
    
    void Update()
    {
        //Get the targets position on screen into a Vector3

        arrowCS = cam.WorldToScreenPoint(arrowWS.transform.position);
        //Get the middle of the screen into a Vector3
        middleScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        //Compute the angle from screenMiddle to targetPos
        var tarAngle = (Mathf.Atan2(arrowCS.x - middleScreen.x, Screen.height - arrowCS.y - middleScreen.y) * Mathf.Rad2Deg) + 90;
        if (tarAngle < 0) tarAngle += 360;


        //Calculate the angle from the camera to the target
        //var targetDir = arrowWS.transform.position - cam.transform.position;
        //var forward = cam.transform.forward;
        //var angle = Vector3.Angle(targetDir, forward);
        //////If the angle exceeds 90deg inverse the rotation to point correctly
        //if (angle< 90){
        //    arrImg.transform.localRotation = Quaternion.Euler(0,0, angle);
        //} else
        //{
        //    arrImg.transform.localRotation = Quaternion.Euler(0,0,-angle);
        //}

        

        var targetPosLocal = cam.transform.InverseTransformPoint(arrowWS.position);
        var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg ;
        this.transform.localRotation = Quaternion.Euler(0, 0, targetAngle);

      

    }

    
    


}


