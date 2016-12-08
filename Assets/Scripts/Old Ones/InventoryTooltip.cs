using UnityEngine;
using System.Collections;

public class InventoryTooltip : MonoBehaviour {

    public UIHandler refUIHandler;
    public string textTooltip;
    public SpriteRenderer[] outline;

    void Start()
    {
        refUIHandler = FindObjectOfType<UIHandler>();
        outline = GetComponentsInChildren<SpriteRenderer>();
        outline[1].color = Color.clear;
    }

	void OnMouseOver()
    {
        if (refUIHandler)
        {
            //refUIHandler.CluePanelActivator(textTooltip);
        }
        
    }
    void OnMouseExit()
    {
        refUIHandler.CluePanelDeactivator();
    }
}
