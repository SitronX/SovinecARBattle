using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchRotate : MonoBehaviour
{
    float multiplier = 0.002f;
    Vector3 _startPosition;
    bool rotation = false;
    public Action<bool> rotating;


    void Update()
    {
        
        if(Input.touchCount==2)
        {
            rotating?.Invoke(true);
            rotation = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);


            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;            //SCALING
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            Zoom(difference * multiplier);


            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)      //Rotating
            {
                _startPosition = touchOne.position - touchZero.position;
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                Vector2 currVector = touchOne.position - touchZero.position;
                float angle = Vector2.SignedAngle(_startPosition, currVector);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + angle, 0);
                _startPosition = currVector;
            }        
        }
        else if(rotation)
        {
            if(Input.touchCount==0)
            {
                rotation = false;
                rotating.Invoke(false);
            }
        }
    }
    void Zoom(float increment)
    {
        float tmp = Mathf.Clamp(transform.localScale.x - increment, 0.1f, 10);
        transform.localScale = new Vector3(tmp, tmp, tmp);
    }
    
}
