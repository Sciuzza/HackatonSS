using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    public GameObject panelClue;
    public bool isShowingClue = false;
    bool isToClosePanel = false;

    public IEnumerator TimedClue(string text)
    {
        char[] charArray = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            isShowingClue = true;
            charArray[i] = text[i];
            panelClue.GetComponentInChildren<Text>().text += charArray[i];
            yield return new WaitForSeconds(Random.Range(0.05f,0.15f));
        }
        while (!isToClosePanel)
        {
            yield return null;
        }
        isShowingClue = false;
        isToClosePanel = false;
        panelClue.GetComponentInChildren<Text>().text = "";
        CluePanelDeactivator();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && panelClue.activeInHierarchy)
        {
            isToClosePanel = true;
        }
    }

    public void CluePanelActivator(string text)
    {
        panelClue.SetActive(true);
        StartCoroutine(TimedClue(text));
    }
    public void CluePanelDeactivator()
    {
        panelClue.SetActive(false);
    }
}
