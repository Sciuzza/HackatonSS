using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    public GameObject panelClue;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
