using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneContN : MonoBehaviour {

    #region Taking References and Linking Events
    void Awake()
    {
        GameContN gcTempLink = this.gameObject.GetComponent<GameContN>();

        gcTempLink.loadingMenuRequest.AddListener(LoadSceneByIndex);

        UiContN uiTempLink = this.gameObject.GetComponent<UiContN>();

        uiTempLink.loadingMapRequest.AddListener(LoadSceneByIndex);
        uiTempLink.gameplayRequest.AddListener(LoadSceneByIndex);

    }
    #endregion

    #region Switch Scene Methods
    private void LoadSceneByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    private void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    } 
    #endregion
}
