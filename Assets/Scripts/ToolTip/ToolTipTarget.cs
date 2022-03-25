using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTarget : MonoBehaviour
{
    ToolTip tt;
    [SerializeField] List<string> tooltipValues = new List<string>();
    TextMeshProUGUI currentText;

    void Start()
    {
        tt = GameObject.Find("Tooltip(DO NOT CHANGE NAME)").GetComponent<ToolTip>();
        currentText = GetComponent<TextMeshProUGUI>();
        TapToPlace.objectTouched += ElementTouched;
    }

  
    public void ElementTouched(List<RaycastResult>result, Vector2 position)
    {
        foreach(RaycastResult i in result)
        {
            if(i.gameObject==gameObject)
            {
                int link = TMP_TextUtilities.FindIntersectingLink(currentText, position, null);
                if (link != -1)
                {
                    string actualLinkId = currentText.textInfo.linkInfo[link].GetLinkID();      //Every link needs to have unique ID set in textmesh pro, because of changing subtitles
                    tt.ShowTooltip(tooltipValues[int.Parse(actualLinkId)], position);
                }
            }
        }
    }
}
