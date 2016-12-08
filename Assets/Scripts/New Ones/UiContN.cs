using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UiContN : MonoBehaviour {

    #region Private Variables
    private GameObject milanMap, romeMap, newsInfo;
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

        SettingCurrentCity(currentCity);
    }

    private void SettingCurrentCity(cities currentCity)
    {
        switch (currentCity)
        {
            case cities.Milan:

                romeMap.SetActive(false);
                milanMap.SetActive(true);
                newsInfo = GameObject.FindGameObjectWithTag("NewsInfo");
                newsInfo.SetActive(false);


                // ad Hoc Code for demo purpose
                Button delittoCat = GameObject.FindGameObjectWithTag("DelittoCat").GetComponent<Button>();
                delittoCat.onClick.AddListener(gameplayStartRequest);
                

                Button switchToRome = GameObject.FindGameObjectWithTag("SR Button").GetComponent<Button>();
                switchToRome.onClick.AddListener(SwitchToRome);

                GameContN.Debugging("here");

                break;

            case cities.Rome:

                milanMap.SetActive(false);
                romeMap.SetActive(true);
                newsInfo = GameObject.FindGameObjectWithTag("NewsInfo");
                newsInfo.SetActive(false);


                Button switchToMilan = GameObject.FindGameObjectWithTag("SM Button").GetComponent<Button>();
                switchToMilan.onClick.AddListener(SwitchToMilan);

                break;
        }
    }

    private void gameplayStartRequest()
    {
        gameplayRequest.Invoke(3);
    }

    private void SwitchToRome()
    {
        GameContN.Debugging("sr Pressed");
        SettingCurrentCity(cities.Rome);
    }

    private void SwitchToMilan()
    {
        SettingCurrentCity(cities.Milan);
    }
    #endregion

}
