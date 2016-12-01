using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewsSelector : MonoBehaviour
{
    public int newsId;
    
    public bool isClicked = false;
    public UIHandler refUIHandler;
    public SpriteRenderer[] outline;
    public string textTooltip;
  

    void Start()
    {
        outline = GetComponentsInChildren<SpriteRenderer>();
        refUIHandler = FindObjectOfType<UIHandler>();

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
            refUIHandler.CluePanelActivator(textTooltip);
            GetComponent<AudioSource>().Play();
            isClicked = true;
        }
        else if (isClicked)
        {

            SceneManager.LoadScene("PerspectiveSelection");

            isClicked = false;
        }

    }
}
