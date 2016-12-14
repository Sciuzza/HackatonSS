using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UiEditorScript: MonoBehaviour
{
    public Image[] imageArray = new Image[10];
    public Color panelColor;
    public Color outLineColor;
    public float outLineSize;
    
    void Update ()
    {
        foreach (var panel in imageArray)
        {
            panel.color = panelColor;
            panel.fillCenter = false;
            panel.GetComponent<Outline>().effectColor = outLineColor;
            panel.GetComponent<Outline>().effectDistance = new Vector2(outLineSize, -outLineSize);
            panel.GetComponent<Outline>().useGraphicAlpha = true;
        }
	}
   
}
