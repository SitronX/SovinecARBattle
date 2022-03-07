using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class ModelVisibility : MonoBehaviour
{
    // Start is called before the first frame update
    private static List<GameObject> trackedObjects = new List<GameObject>();
    public static Action<bool> modelVisible;
    private static object lockingObject = new object { };

    private void OnBecameInvisible()
    {
        

        lock (lockingObject)
        {
            trackedObjects.Remove(gameObject);
            if (trackedObjects.Count == 0)
            {
                modelVisible?.Invoke(false);
            }
        }

    }
    private void OnBecameVisible()
    {
        lock (lockingObject)
        {
            if (trackedObjects.Count == 0)
            {
                trackedObjects.Add(gameObject);
                modelVisible?.Invoke(true);
            }
        }

    }
}
