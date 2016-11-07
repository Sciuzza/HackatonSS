using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    public GameObject panelClue;
	
	


    public void CluePanelActivator(string text)
    {
        panelClue.SetActive(true);
        panelClue.GetComponentInChildren<Text>().text = text;
    }
    public void CluePanelDeactivator()
    {
        panelClue.SetActive(false);
    }
}
