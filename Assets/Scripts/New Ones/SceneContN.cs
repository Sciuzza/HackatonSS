using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneContN : MonoBehaviour {

    void Awake()
    {
        GameContN gcTempLink = this.gameObject.GetComponent<GameContN>();

        gcTempLink.loadingMenuRequest.AddListener(LoadSceneByIndex);

        UiContN uiTempLink = this.gameObject.GetComponent<UiContN>();

        uiTempLink.loadingMap.AddListener(LoadSceneByIndex);

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
