using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UiContN : MonoBehaviour
{

    #region Private Variables
    private GameObject map, newsInfo, cityTextI, newsMilTemp, newsRomTemp;
    private Button srButton, smButton, playNews;
    private Button delittoCatB;
    private readonly float[] newsPanelPos = { 0.125f, -0.125f, 1.0165f, -0.1155005f };
    #endregion

    #region Public Variables
    public GameObject newsMil, newsRom;
    public Sprite mapMil, mapRom;
    #endregion

    #region Events
    public event_int loadingMapRequest, gameplayRequest;
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
        srButton = GameObject.FindGameObjectWithTag("SR Button").GetComponent<Button>();
        smButton = GameObject.FindGameObjectWithTag("SM Button").GetComponent<Button>();
        playNews = GameObject.FindGameObjectWithTag("PlayNews").GetComponent<Button>();

        newsInfo.SetActive(false);
        srButton.onClick.AddListener(SwitchToRome);
        smButton.onClick.AddListener(SwitchToMilan);
        playNews.interactable = false;

    

        SettingCurrentCity();
    }

    private void SettingCurrentCity()
    {
        switch (GameContN.playerDatasStatic.lastCityVisited)
        {
            case cities.Milan:

                if (newsRomTemp != null)
                    Destroy(newsRomTemp);

                if (newsInfo.activeInHierarchy)
                    newsInfo.SetActive(false);

                newsMilTemp = Instantiate(newsMil);
                newsMilTemp.transform.SetParent(map.transform);
                newsMilTemp.GetComponent<RectTransform>().offsetMin = new Vector2(newsPanelPos[0], newsPanelPos[3]);
                newsMilTemp.GetComponent<RectTransform>().offsetMax = new Vector2(-newsPanelPos[1], -newsPanelPos[2]);
                newsMilTemp.name = "NewsMil";

                smButton.interactable = false;
                srButton.interactable = true;
                playNews.interactable = false;

                map.GetComponent<Image>().sprite = mapMil;



                delittoCatB = newsMilTemp.GetComponentInChildren<Button>();
              
                delittoCatB.onClick.AddListener(EnablingNewsInfo);

              

                break;

            case cities.Rome:

                if (newsMilTemp != null)
                    Destroy(newsMilTemp);

                if (newsInfo.activeInHierarchy)
                    newsInfo.SetActive(false);

                newsRomTemp = Instantiate(newsRom);
                newsRomTemp.transform.SetParent(map.transform);
                newsRomTemp.GetComponent<RectTransform>().offsetMin = new Vector2(newsPanelPos[0], newsPanelPos[3]);
                newsRomTemp.GetComponent<RectTransform>().offsetMax = new Vector2(-newsPanelPos[1], -newsPanelPos[2]);
                newsRomTemp.name = "NewsRom";

                srButton.interactable = false;
                smButton.interactable = true;
                playNews.interactable = false;

                map.GetComponent<Image>().sprite = mapRom;

                break;
        }
    }

    private void SwitchToRome()
    {
        GameContN.playerDatasStatic.lastCityVisited = cities.Rome;
        SettingCurrentCity();
    }

    private void SwitchToMilan()
    {
        GameContN.playerDatasStatic.lastCityVisited = cities.Milan;
        SettingCurrentCity();
    }

    private void EnablingNewsInfo()
    {
        newsInfo.SetActive(true);
        newsInfo.GetComponentInChildren<Text>().text = GameContN.playerDatasStatic.mapData[0].newsData[0].newsInfoText;
        playNews.interactable = true;
        playNews.onClick.RemoveAllListeners();
        playNews.onClick.AddListener(gameplayStartRequest);
    }

    private void gameplayStartRequest()
    {
        // metti qui fede
        GameContN.Debugging("Ci Siamo");
    }

    #endregion

    #region Gameplay Methods
    private void GamePlayInitializer()
    {
        // qui ci sei tu fabri
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
}
