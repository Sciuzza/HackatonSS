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

        //rtTempLink.anchorMin = new Vector2(0, 0);
        //rtTempLink.anchorMax = new Vector2(1, 1);
        rtTempLink.

        Debug.Log(((rtTempLink.anchorMax.x * 1000) / 1000));
        Debug.Log(rtTempLink.anchorMin);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        Debug.Log(rtTempLink.offsetMax);

        float rectMaxX = Screen.width - ((Mathf.Abs((rtTempLink.offsetMax.x / (rtTempLink.anchorMax.x * 1000) / 1000 )) * 1000) / 1000);
        float rectMaxY = Screen.height - Mathf.Abs(rtTempLink.offsetMax.y / rtTempLink.anchorMax.y );

        Debug.Log(rectMaxX);


        Vector2 anchorMinPos = new Vector2((rtTempLink.position.x) / Screen.width, (rtTempLink.position.y) / Screen.height);
        Vector2 anchorMaxPos = new Vector2(rectMaxX / Screen.width, rectMaxY / Screen.height);
        rtTempLink.offsetMin = new Vector2(0, 0);
        rtTempLink.offsetMax = new Vector2(0, 0);
        rtTempLink.anchorMin = anchorMinPos;
        rtTempLink.anchorMax = anchorMaxPos;

    }


}
