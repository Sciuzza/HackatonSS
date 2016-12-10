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

        Vector2 anchorMinTemp = rtTempLink.anchorMin;
        Vector2 anchorMaxTemp = rtTempLink.anchorMax;

        Vector2 offsetMinTemp = rtTempLink.offsetMin;
        Vector2 offsetMaxTemp = rtTempLink.offsetMax;

         rtTempLink.anchorMin = new Vector2(0, 0);
         rtTempLink.anchorMax = new Vector2(1, 1);

        /*Debug.Log(-offsetMaxTemp.x);
        Debug.Log((rtTempLink.anchorMax.x - ((anchorMaxTemp.x * 1000) / 1000)) * Screen.width);
        */
        
        rtTempLink.offsetMax = new Vector2(-(-offsetMaxTemp.x + (rtTempLink.anchorMax.x - ((anchorMaxTemp.x * 1000) / 1000)) * Screen.width),
                               -(-offsetMaxTemp.y + (rtTempLink.anchorMax.y - ((anchorMaxTemp.y * 1000) / 1000)) * Screen.height));
        rtTempLink.offsetMin = new Vector2(-(-offsetMinTemp.x + (rtTempLink.anchorMin.x - ((anchorMinTemp.x * 1000) / 1000)) * Screen.width), 
                               -(-offsetMinTemp.y + (rtTempLink.anchorMin.y - ((anchorMinTemp.y * 1000) / 1000)) * Screen.height));
                          
        //rtTempLink.SetAnchor(AnchorPresets.StretchAll);
        /*
        Debug.Log(((rtTempLink.anchorMax.x * 1000) / 1000));
        Debug.Log(rtTempLink.anchorMin);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        Debug.Log(rtTempLink.offsetMax);
        */
        float rectMaxX = Screen.width + rtTempLink.offsetMax.x;
        float rectMaxY = Screen.height + rtTempLink.offsetMax.y;

       // Debug.Log(rectMaxX);

        
        Vector2 anchorMinPos = new Vector2((rtTempLink.position.x) / Screen.width, (rtTempLink.position.y) / Screen.height);
        Vector2 anchorMaxPos = new Vector2(rectMaxX / Screen.width, rectMaxY / Screen.height);
        rtTempLink.offsetMin = new Vector2(0, 0);
        rtTempLink.offsetMax = new Vector2(0, 0);
        rtTempLink.anchorMin = anchorMinPos;
        rtTempLink.anchorMax = anchorMaxPos;
        
    }


}
