using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UiContN : MonoBehaviour {

    #region Events
    public event_int loadingMap;
    public UnityEvent quitGame; 
    #endregion

    #region Taking References and Linking Events
    private void Awake()
    {

        GameContN gcTempLink = this.gameObject.GetComponent<GameContN>();

        gcTempLink.mmInitRequest.AddListener(MainMenuInitializer);

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
        loadingMap.Invoke(2);
    }

    private void QuitGameRequest()
    {
        quitGame.Invoke();
    } 
    #endregion

}
