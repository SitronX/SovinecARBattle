using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotationTest : MonoBehaviour
{
    // Start is called before the first frame update
    bool rightClick = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)||!rightClick)
        {
            if(rightClick)
            {
                rightClick = false;
            }
           
            Vector3 rot = transform.rotation.eulerAngles;
            


            
            rot.y += 0.05f;

            //transform.rotation = Quaternion.Euler(rot);

            transform.Rotate(new Vector3(0,5,0),Space.Self);
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            rightClick = true;
        }
    }
}
