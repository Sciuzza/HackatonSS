using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIHandler : MonoBehaviour
{

    public GameObject panelClue;
    public bool isShowingClue = false;
    bool isToClosePanel = false;
    public RectTransform inventoryRef;
    public AudioSource audioManager;
    public AudioClip pickUpClue;
    public AudioClip turnPage;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioSource>();
    }

    public IEnumerator TimedClue(string text)
    {
        panelClue.GetComponentInChildren<Text>().text = "";
        char[] charArray = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            isShowingClue = true;
            charArray[i] = text[i];
            panelClue.GetComponentInChildren<Text>().text += charArray[i];
            yield return new WaitForSeconds(Random.Range(0.02f,0.10f));
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

    public void InventoryWrapper()
    {
        if (!movingInventory)
        {
            if (inventoryInside)
            {
                StartCoroutine(InventoryPanelDeactivator());
            }
            else
            {
                StartCoroutine(InventoryPanelActivator());
            }
        }
    }

    bool movingInventory;
    public int inventoryMovingSpeed = 300;

    public IEnumerator InventoryPanelActivator()
    {
        movingInventory = true;
        while (inventoryRef.anchoredPosition.x > 0)
        {
            inventoryRef.anchoredPosition += new Vector2(-inventoryMovingSpeed, 0)*Time.deltaTime;
            yield return null;
        }
        inventoryInside = true;
        movingInventory = false;
        inventoryRef.anchoredPosition = new Vector2(0, 0);
    }

    public IEnumerator InventoryPanelDeactivator()
    {
        movingInventory = true;
        while (inventoryRef.anchoredPosition.x < 60)
        {
            inventoryRef.anchoredPosition += new Vector2(inventoryMovingSpeed, 0) * Time.deltaTime;
            yield return null;
        }
        inventoryInside = false;
        movingInventory = false;
        inventoryRef.anchoredPosition = new Vector2(220, 0);
    }

    public UnityEvent levelFinished;
    public bool canQuitScene;
    public string lastClueTextTooltip;

    public void ShowLastClue()
    {
        if (!isShowingClue && lastClueTextTooltip != "")
        {
            CluePanelActivator(lastClueTextTooltip);
            audioManager.clip = turnPage;
            audioManager.Play();
        }
    }

    public void EnablingExit()
    {
        canQuitScene = true;
    }

    public void ChangeScene()
    {
        if (canQuitScene)
        {
            levelFinished.Invoke();
        }
    }
}
