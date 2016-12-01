using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    public GameObject panelClue;
    public bool isShowingClue = false;
    bool isToClosePanel = false;
    public RectTransform inventoryRef;

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

    public bool inventoryInside;

    public IEnumerator InventoryPanelActivator()
    {
        inventoryInside = true;
        while (inventoryRef.anchoredPosition.y > 0)
        {
            Debug.Log("Dio");
            inventoryRef.anchoredPosition += new Vector2(0, 20)*Time.deltaTime;
            yield return null;
        }
    }
}
