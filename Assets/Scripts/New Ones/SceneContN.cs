using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneContN : MonoBehaviour {

    void Awake()
    {
        GameContN gcTempLink = this.gameObject.GetComponent<GameContN>();

        gcTempLink.loadingMenuRequest.AddListener(LoadSceneByIndex);

        UiContN uiTempLink = this.gameObject.GetComponent<UiContN>();

        uiTempLink.loadingMapRequest.AddListener(LoadSceneByIndex);
        uiTempLink.gameplayRequest.AddListener(LoadSceneByIndex);

    }

    private void LoadSceneByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    private void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
