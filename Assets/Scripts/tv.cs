using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class tv : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }
    int TryGetTouchPosition(out Vector2 touchPos)
    {
        if (Input.touchCount == 1)
        {
            touchPos = Input.GetTouch(0).position;
            return 1;
        }
        if (Input.touchCount > 1)
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
        if (TryGetTouchPosition(out Vector2 touchPos) == 0)
        {
            return;
        }
        else if (TryGetTouchPosition(out Vector2 touchPos1) == 1)
        {
            Ray ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
            try
            {
                if (Physics.Raycast(ray, out RaycastHit hitObject))
                {
                    hitObject.transform.GetComponent<VideoPlayer>().Play();
                }
            }
            catch { }

        }

        
        
    }
}
