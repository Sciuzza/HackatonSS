using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UiContN : MonoBehaviour
{

    #region Private Variables
    private GameObject milanMap, romeMap, newsInfoMil, newsInfoRom;
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
    private void MapInitializer(cities currentCity)
    {
        milanMap = GameObject.FindGameObjectWithTag("MapMil");
        romeMap = GameObject.FindGameObjectWithTag("MapRom");

        // ad Hoc Code for demo purpose

        GameObject delittoCatGO = GameObject.FindGameObjectWithTag("DelittoCat");

        Button delittoCatB = delittoCatGO.GetComponent<Button>();
        delittoCatB.onClick.AddListener(gameplayStartRequest);

        NewsInfoShow delittoCatN = delittoCatGO.GetComponent<NewsInfoShow>();

        delittoCatN.newsInfoText = GameContN.playerDatasStatic.mapData[0].newsData[0].newsInfoText;

        // Only for Mouse Users
        delittoCatN.newsInfoDisableRequest.AddListener(DisablingNewsInfoMilan);
        delittoCatN.newsInfoShowRequest.AddListener(EnablingNewsInfoMilan);

        Button switchToRome = GameObject.FindGameObjectWithTag("SR Button").GetComponent<Button>();
        switchToRome.onClick.AddListener(SwitchToRome);

        newsInfoMil = GameObject.FindGameObjectWithTag("NewsInfoMil");
        newsInfoMil.SetActive(false);

        Button switchToMilan = GameObject.FindGameObjectWithTag("SM Button").GetComponent<Button>();
        switchToMilan.onClick.AddListener(SwitchToMilan);

        newsInfoRom = GameObject.FindGameObjectWithTag("NewsInfoRom");
        newsInfoRom.SetActive(false);

        SettingCurrentCity(currentCity);
    }

    private void SettingCurrentCity(cities currentCity)
    {
        switch (currentCity)
        {
            case cities.Milan:

                romeMap.SetActive(false);
                milanMap.SetActive(true);
            
                break;

            case cities.Rome:

                milanMap.SetActive(false);
                romeMap.SetActive(true);
              
                break;
        }
    }

    private void gameplayStartRequest()
    {
        gameplayRequest.Invoke(3);
    }

    private void SwitchToRome()
    {
        SettingCurrentCity(cities.Rome);
    }

    private void SwitchToMilan()
    {
        SettingCurrentCity(cities.Milan);
    }

    private void DisablingNewsInfoMilan()
    {
        newsInfoMil.SetActive(false);
    }

    private void EnablingNewsInfoMilan(string whatToSay)
    {
        newsInfoMil.SetActive(true);
        newsInfoMil.GetComponentInChildren<Text>().text = whatToSay;
    }

    #endregion

}
