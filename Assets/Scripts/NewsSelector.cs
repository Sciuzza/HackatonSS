using UnityEngine;
using System.Collections;

public class NewsSelector : MonoBehaviour
{

    
    public bool isClicked = false;
    public UIHandler refUIHandler;
    public SpriteRenderer[] outline;


    void Start()
    {
        outline = GetComponentsInChildren<SpriteRenderer>();
    }
    void OnMouseOver()
    {
        outline[1].color = Color.white;
    }
    void OnMouseExit()
    {
        outline[1].color = Color.clear;
    }



    void OnMouseUp()
    {
        if (!isClicked)
        {

            isClicked = true;
        }
        else if (isClicked)
        {
           

            isClicked = false;
        }

    }
}
