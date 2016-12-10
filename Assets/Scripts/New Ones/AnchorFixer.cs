using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnchorFixer : MonoBehaviour
{

 
    void Start()
    {

        RectTransform rtTempLink = this.GetComponent<RectTransform>();

        rtTempLink.pivot = new Vector2(0, 0);

        float rectMaxX = Screen.width + rtTempLink.offsetMax.x;
        float rectMaxY = Screen.height + rtTempLink.offsetMax.y;

        Vector2 anchorMinPos = new Vector2((rtTempLink.position.x) / Screen.width, (rtTempLink.position.y) / Screen.height);
        Vector2 anchorMaxPos = new Vector2(rectMaxX / Screen.width, rectMaxY / Screen.height);
        rtTempLink.offsetMin = new Vector2(0, 0);
        rtTempLink.offsetMax = new Vector2(0, 0);
        rtTempLink.anchorMin = anchorMinPos;
        rtTempLink.anchorMax = anchorMaxPos;

    }


}
