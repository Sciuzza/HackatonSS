using UnityEngine;
using System.Collections;

public class ClickableHandler : MonoBehaviour
{
    Inventory refInv;
    public bool isClicked = false;
    public SpriteRenderer[] outline;
    public UIHandler refUIHandler;
    public string textTooltip;
    public AudioSource audioManager;
    public AudioClip pickUpClue;
    public AudioClip turnPage;
   
    void Start()
    {
        refUIHandler = FindObjectOfType<UIHandler>();
        outline = GetComponentsInChildren<SpriteRenderer>();
        refInv = FindObjectOfType<Inventory>();
        audioManager = GetComponent<AudioSource>();
    }
    void OnMouseOver()
    {
        if (!refUIHandler.isShowingClue)
        {
            outline[1].color = Color.white;
        }
    }
    void OnMouseExit()
    {
        if (!refUIHandler.isShowingClue)
        {
            outline[1].color = Color.clear;
        }
    }

    void OnMouseUp()
    {
        if (!refUIHandler.isShowingClue && !isClicked)
        {
            outline[1].color = Color.clear;
            refUIHandler.CluePanelActivator(textTooltip);
            audioManager.clip = turnPage;
            audioManager.Play();
            if (!isClicked)
            {
                foreach (var item in refInv.clickableContainer)
                {
                    item.isClicked = false;
                }
                isClicked = true;
                refInv.SetInventory(this.gameObject);
            }
        }
    }
}
