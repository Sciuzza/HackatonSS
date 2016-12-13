using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UiContN : MonoBehaviour
{

    #region Private Variables

    #region Map Variables
    private GameObject map, newsInfo, cityTextI, newsMilTemp, newsRomTemp;
    private Button srButton, smButton, playNews;
    private Button[] mapButtons;
    private Button delittoCatB;
    private readonly float[] newsPanelPos = { 0.125f, -0.125f, 1.0165f, -0.1155005f };
    #endregion

    #region GamePlay Variables
    List<GameObject> clueInScene;
    private CustomClickEvent[] switchSceneButtons;
    private GameObject clueInfoPanel, bigInventoryButton, blockButton;
    private ClueCustomClickEvent lastClueButton;
    private Button invOpenButton;
    private GameObject inventory;
    private bool isShowingClue = false, isToClosePanel = false;
    private bool movingInventory = false;
    private bool inventoryInside;
    private float inventoryMovingSpeed = 300;
    Coroutine disableInfoPanelCO, timedInfoTextCO;
    bool isShowingInventory = false;
    bool isShowingLastClue = false;
    bool isInventoryOpen = false;
    Button[] inventorySlots;
    int slotToOccupied;
    GameObject lastClueClicked;

    #endregion

    #endregion

    #region Public Variables

    #region Map Variables
    public GameObject newsMil, newsRom;
    public Sprite mapMil, mapRom;
    public GameObject[] mapPrefabs;
    public Sprite[] mapImages;
    #endregion

    #region Gameplay Variables

    #endregion

    #endregion

    #region Events
    public event_int loadingMapRequest, gameplayRequest, loadingSceneRequest;
    public UnityEvent quitGame;
    #endregion

    #region Taking References and Linking Events
    private void Awake()
    {

        GameContN gcTempLink = this.gameObject.GetComponent<GameContN>();

        gcTempLink.mmInitRequest.AddListener(MainMenuInitializer);
        gcTempLink.mapInitRequest.AddListener(MapInitializer);
        gcTempLink.gameplayInitRequest.AddListener(GamePlayInitializer);
        gcTempLink.readingNewsRequest.AddListener(ReadingNewsInitializer);
        gcTempLink.scoreRequest.AddListener(ScoreInitializer);
    }
    #endregion

    #region Main Menu Methods
    private void MainMenuInitializer()
    {
        GameObject mmPanelTempLink = GameObject.FindGameObjectWithTag("MainMenu");

        Button[] buttons = mmPanelTempLink.GetComponentsInChildren<Button>();

        buttons[0].onClick.AddListener(MapRequest);
        buttons[1].onClick.AddListener(QuitGameRequest);

    }

    private void MapRequest()
    {
        loadingMapRequest.Invoke(2);
    }

    private void QuitGameRequest()
    {
        quitGame.Invoke();
    }
    #endregion

    #region Map Methods
    private void MapInitializer()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        newsInfo = GameObject.FindGameObjectWithTag("NewsInfo");
        cityTextI = GameObject.FindGameObjectWithTag("CityTextI");

       // srButton = GameObject.FindGameObjectWithTag("SR Button").GetComponent<Button>();
      //  smButton = GameObject.FindGameObjectWithTag("SM Button").GetComponent<Button>();

        mapButtons = GameObject.FindGameObjectWithTag("MapButtons").GetComponentsInChildren<Button>();

        playNews = GameObject.FindGameObjectWithTag("PlayNews").GetComponent<Button>();

        newsInfo.SetActive(false);
        // srButton.onClick.AddListener(SwitchToRome);
        // smButton.onClick.AddListener(SwitchToMilan);

        for (int i = 0; i < mapButtons.Length; i++)
        {
            mapButtons[i].gameObject.GetComponent<CustomClickEvent>().buttonIndex = i;
            mapButtons[i].gameObject.GetComponent<CustomClickEvent>().customClick.AddListener(SwitchingCity);
        }

       
      

        playNews.interactable = false;



        SettingCurrentCity();
    }

    private void SettingCurrentCity()
    {
        switch (GameContN.playerDatasStatic.lastCityVisited)
        {
            case "milan":

                if (newsRomTemp != null)
                    Destroy(newsRomTemp);

                if (newsInfo.activeInHierarchy)
                    newsInfo.SetActive(false);

                newsMilTemp = Instantiate(newsMil);
                newsMilTemp.transform.SetParent(map.transform);
                newsMilTemp.GetComponent<RectTransform>().offsetMin = new Vector2(newsPanelPos[0], newsPanelPos[3]);
                newsMilTemp.GetComponent<RectTransform>().offsetMax = new Vector2(-newsPanelPos[1], -newsPanelPos[2]);
                newsMilTemp.name = "NewsMil";

                //smButton.interactable = false;
                //srButton.interactable = true;
                mapButtons[0].gameObject.SetActive(false);
                mapButtons[1].gameObject.SetActive(true);
                playNews.interactable = false;

                map.GetComponent<Image>().sprite = mapMil;



                delittoCatB = newsMilTemp.GetComponentInChildren<Button>();

                delittoCatB.onClick.AddListener(EnablingNewsInfo);



                break;

            case "rome":

                if (newsMilTemp != null)
                    Destroy(newsMilTemp);

                if (newsInfo.activeInHierarchy)
                    newsInfo.SetActive(false);

                newsRomTemp = Instantiate(newsRom);
                newsRomTemp.transform.SetParent(map.transform);
                newsRomTemp.GetComponent<RectTransform>().offsetMin = new Vector2(newsPanelPos[0], newsPanelPos[3]);
                newsRomTemp.GetComponent<RectTransform>().offsetMax = new Vector2(-newsPanelPos[1], -newsPanelPos[2]);
                newsRomTemp.name = "NewsRom";

                // srButton.interactable = false;
                // smButton.interactable = true;
                mapButtons[0].gameObject.SetActive(true);
                mapButtons[1].gameObject.SetActive(false);
                playNews.interactable = false;

                map.GetComponent<Image>().sprite = mapRom;

                break;
        }
    }

    private void SwitchingCity(int cityIndex)
    {
        GameContN.playerDatasStatic.lastCityVisited = GameContN.playerDatasStatic.mapData[cityIndex].mapName;
        SettingCurrentCity();
    }

    private void SwitchToRome()
    {
        GameContN.playerDatasStatic.lastCityVisited = "rome";
        SettingCurrentCity();
    }

    private void SwitchToMilan()
    {
        GameContN.playerDatasStatic.lastCityVisited = "milan";
        SettingCurrentCity();
    }

    private void EnablingNewsInfo()
    {
        newsInfo.SetActive(true);
        newsInfo.GetComponentInChildren<Text>().text = GameContN.playerDatasStatic.mapData[0].newsData[0].newsInfoText;
        playNews.interactable = true;
        GameContN.playerDatasStatic.lastNewsVisited = GameContN.playerDatasStatic.mapData[0].newsData[0].newsName;
        playNews.onClick.RemoveAllListeners();
        playNews.GetComponent<CustomClickEvent>().buttonIndex = 3;
        playNews.GetComponent<CustomClickEvent>().customClick.AddListener(loadingSceneRequestMethod);
    }

    #endregion

    #region Gameplay Methods
    private void GamePlayInitializer()
    {
        /*
        Debug.Log("Gameplay Initializer");
        // qui ci sei tu fabri
        GameObject[] tempClue = GameObject.FindGameObjectsWithTag("Clue");
        clueInScene = new Button[tempClue.Length];
        for (int i = 0; i < tempClue.Length; i++)
        {
            clueInScene[i] = tempClue[i].GetComponentInChildren<Button>();
            tempClue[i].AddComponent<ClickableHandler>();
            clueInScene[i].onClick.AddListener(Test);
            //mapButtons[i].gameObject.GetComponent<CustomClickEvent>().customClick.AddListener(SwitchingCity);
        }
        */


        InventoryInitializer();
        CluesInitializer();
        StatiButtonInitializer();
    }

    private void StatiButtonInitializer()
    {
        switchSceneButtons = new CustomClickEvent[3];

        switchSceneButtons[0] = GameObject.Find("Menu").GetComponent<CustomClickEvent>();
        switchSceneButtons[1] = GameObject.Find("NextScene").GetComponent<CustomClickEvent>();
        switchSceneButtons[2] = GameObject.Find("PrevScene").GetComponent<CustomClickEvent>();

        bigInventoryButton = GameObject.FindGameObjectWithTag("BigInventoryButton");
        bigInventoryButton.GetComponent<Button>().onClick.AddListener(DisablingTransition);
        bigInventoryButton.GetComponent<Button>().onClick.AddListener(ButtonInventoryOpener);        
        bigInventoryButton.SetActive(false);

        blockButton = GameObject.FindGameObjectWithTag("BlockButton");
        blockButton.GetComponent<Button>().onClick.AddListener(ButtonBlockerMethods);
        blockButton.SetActive(false);

        switchSceneButtons[0].buttonIndex = 1;
        switchSceneButtons[0].customClick.AddListener(loadingSceneRequestMethod);
        switchSceneButtons[1].buttonIndex = SceneManager.GetActiveScene().buildIndex + 1;
        switchSceneButtons[1].customClick.AddListener(loadingSceneRequestMethod);
        switchSceneButtons[2].buttonIndex = SceneManager.GetActiveScene().buildIndex - 1;
        switchSceneButtons[2].customClick.AddListener(loadingSceneRequestMethod);

        clueInfoPanel = GameObject.Find("ClueInfo");
        clueInfoPanel.SetActive(false);


        lastClueButton = GameObject.Find("LastClue").GetComponent<ClueCustomClickEvent>();
        lastClueButton.customClick.AddListener(LastClueVisualizer);

        invOpenButton = GameObject.FindGameObjectWithTag("InventoryButton").GetComponent<Button>();
        invOpenButton.onClick.AddListener(InventoryHandler);


    }
    
    void InventoryHandler()
    {
        if (!isInventoryOpen)
        {            
            if (!movingInventory)
            {
                StartCoroutine(InventoryPanelActivator());
            }
        }
        else if(isInventoryOpen)
        {
            if (!movingInventory)
            {
                StartCoroutine(InventoryPanelDeactivator());
            }
        }

        
        

    }
    private void ButtonBlockerMethods()
    {
        
        if (!isShowingInventory)
        {
            StartCoroutine(InventoryPanelDeactivator());            
        }
        
    }

    private void DisablingTransition()
    {
        lastClueClicked.GetComponent<Button>().transition = Selectable.Transition.None;
    }
    private void ButtonInventoryOpener()
    {
        if (!isInventoryOpen)
        {
            StartCoroutine(InventoryPanelActivator());
            clueInfoPanel.SetActive(false);
            blockButton.SetActive(true);
            bigInventoryButton.SetActive(false);
        }
        
    }

    private void InventoryInitializer()
    {
        slotToOccupied = 0;
        inventorySlots = new Button[7];
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        for (int i = 1; i <= 7; i++)
        {
            inventorySlots[i - 1] = GameObject.Find("Slot" + i).GetComponent<Button>();
        }

    }

    private void CluesInitializer()
    {
         
        foreach (var clue in GameObject.FindObjectsOfType<ClueContainer>())
        {            
            clue.Initialization();
            clue.gameObject.GetComponent<ClueCustomClickEvent>().customClick.AddListener(ClueInfoVisualizer);
        }
    }

    void LastClueVisualizer(string infoToVisualize)
    {
        if (infoToVisualize != "" && !isShowingLastClue)
        {
            isShowingLastClue = true;
            
            if (disableInfoPanelCO != null)
            {
                StopCoroutine(disableInfoPanelCO);
            }
            ClueInfoPanelVisualizer(infoToVisualize);
            disableInfoPanelCO = StartCoroutine(DisableInfoPanelCLue());     
        }
        
        
    }
    IEnumerator DisableInfoPanelCLue()
    {
        yield return new WaitForSeconds(2f);
        isShowingLastClue = false;
        clueInfoPanel.SetActive(false);
    }

    private void ClueInfoVisualizer(string infoToVisualize)
    {
        lastClueButton.clueInfoText = infoToVisualize;
 
        if (infoToVisualize != "")
        {
            

            if (disableInfoPanelCO != null)
            {                
                StopCoroutine(disableInfoPanelCO);
                StopCoroutine(timedInfoTextCO);
            }
            if (isInventoryOpen)
            {
               InventoryHandler();
            }
            inventorySlots[slotToOccupied].GetComponent<ClueCustomClickEvent>().clueInfoText = infoToVisualize;
            inventorySlots[slotToOccupied].GetComponent<ClueCustomClickEvent>().customClick.AddListener(LastClueVisualizer);
            slotToOccupied++;
            ClueInfoPanelVisualizer(infoToVisualize);

            lastClueClicked = clueInScene.Find(x => x.GetComponent<ClueContainer>().clueText == infoToVisualize);
            lastClueClicked.GetComponent<ClueContainer>().clueData.hasBeenFound = true;
            lastClueClicked.GetComponent<ClueCustomClickEvent>().customInteractable = false;

            bigInventoryButton.SetActive(true);
            

        }
        
        
        //StartCoroutine(InventoryPanelActivator());
    }

    void ClueInfoPanelVisualizer(string infoToVisualize)
    {
        
        clueInfoPanel.SetActive(true);
        clueInfoPanel.GetComponentInChildren<Text>().color = Color.clear;
        clueInfoPanel.GetComponentInChildren<Text>().text = infoToVisualize;
        int fontSizeTemp = clueInfoPanel.GetComponentInChildren<Text>().fontSize;
        //float heightTemp = clueInfoPanel.GetComponent<LayoutElement>().preferredHeight;
        // clueInfoPanel.GetComponentInChildren<Text>().resizeTextForBestFit = false;
        //clueInfoPanel.GetComponent<Text>().preferredHeight = heightTemp;
        clueInfoPanel.GetComponentInChildren<Text>().fontSize = fontSizeTemp;
        clueInfoPanel.GetComponentInChildren<Text>().color = Color.white;
        timedInfoTextCO = StartCoroutine(TimedClue(infoToVisualize));
    }

    public IEnumerator TimedClue(string text)
    {
        isShowingClue = false;
        isToClosePanel = false;
        //isShowingInventory = true;
        clueInfoPanel.GetComponentInChildren<Text>().text = "";
        
        char[] charArray = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            isShowingClue = true;
            charArray[i] = text[i];
            clueInfoPanel.GetComponentInChildren<Text>().text += charArray[i];
            yield return new WaitForSeconds(Random.Range(0.02f, 0.10f));
        }
        while (!isToClosePanel)
        {
            yield return null;
        }
        isShowingClue = false;
        isToClosePanel = false;
        clueInfoPanel.GetComponentInChildren<Text>().text = "";
        CluePanelDeactivator();
        
    }

    public IEnumerator InventoryPanelActivator()
    {
        isShowingInventory = true;
        movingInventory = true;
        while (inventory.GetComponent<RectTransform>().anchoredPosition.x > 0)
        {
            inventory.GetComponent<RectTransform>().anchoredPosition += new Vector2(-inventoryMovingSpeed, 0) * Time.deltaTime;
            yield return null;
        }
        inventoryInside = true;
        movingInventory = false;
        inventory.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        isShowingInventory = false;
        isInventoryOpen = true;
    }

    public IEnumerator InventoryPanelDeactivator()
    {
        movingInventory = true;
        while (inventory.GetComponent<RectTransform>().anchoredPosition.x < 320)
        {
            inventory.GetComponent<RectTransform>().anchoredPosition += new Vector2(inventoryMovingSpeed, 0) * Time.deltaTime;
            yield return null;
        }
        inventoryInside = false;
        movingInventory = false;       
        blockButton.SetActive(false);
        inventory.GetComponent<RectTransform>().anchoredPosition = new Vector2(320, 0);
        isInventoryOpen = false;
    }

    private void CluePanelDeactivator()
    {
        clueInfoPanel.SetActive(false);
    }
    #endregion

    #region Reading News Methods
    private void ReadingNewsInitializer()
    {

    }
    #endregion

    #region Score Methods
    private void ScoreInitializer()
    {

    }
    #endregion

    #region General Methods
    private void loadingSceneRequestMethod(int buildIndex)
    {
        if (SceneManager.GetActiveScene().buildIndex>2 || SceneManager.GetActiveScene().buildIndex == -1)
        {
            GameContN.playerDatasStatic.lastSceneVisited++;
        }
        loadingSceneRequest.Invoke(buildIndex);
    }
    #endregion
}
