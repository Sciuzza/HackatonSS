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
        outline[1].color = Color.white;
    }
    void OnMouseExit()
    {
        outline[1].color = Color.clear;
        refUIHandler.CluePanelDeactivator();
        foreach (var item in refInv.clickableContainer)
        {
            item.isClicked = false;
        }
    }

    void OnMouseUp()
    {
        if (!isClicked)
        {
            foreach (var item in refInv.clickableContainer)
            {
                item.isClicked = false;
            }
            refUIHandler.CluePanelActivator(textTooltip);

            audioManager.clip = turnPage;
            audioManager.Play();
            isClicked = true;

        }
        else if (isClicked)
        {
            refUIHandler.CluePanelDeactivator();
            refInv.SetInventory(this.gameObject);
            isClicked = false;
            audioManager.clip = pickUpClue;
            audioManager.Play();
        }

    }

}
