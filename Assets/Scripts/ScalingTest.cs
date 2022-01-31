using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalingTest : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject scalingObject;
    public void ScaleChanged()
    {
        float val= this.GetComponent<Slider>().value;
        Vector3 tmp = new Vector3(val, val, val);
        scalingObject.transform.localScale = tmp;
    }
}
