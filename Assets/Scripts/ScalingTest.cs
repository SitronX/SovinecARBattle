using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ScalingTest : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject scalingObject;

    public static Transform centerScene;
    public static Vector3 lastPosition;
    public void ScaleChanged()
    {
        float val= this.GetComponent<Slider>().value;
        Vector3 tmp = new Vector3(val, val, val);

        scalingObject.transform.localScale = tmp;

        //scalingObject.GetComponent<ARSessionOrigin>().MakeContentAppearAt(centerScene, centerScene.position, centerScene.rotation);

    }
}
