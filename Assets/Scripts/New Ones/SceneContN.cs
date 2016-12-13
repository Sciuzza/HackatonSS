using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneContN : MonoBehaviour {

    public GameObject scenePrefab;

    #region Taking References and Linking Events
    void Awake()
    {
        GameContN gcTempLink = this.gameObject.GetComponent<GameContN>();

        gcTempLink.loadingMenuRequest.AddListener(LoadSceneByIndex);

        UiContN uiTempLink = this.gameObject.GetComponent<UiContN>();

        uiTempLink.loadingMapRequest.AddListener(LoadSceneByIndex);
        uiTempLink.gameplayRequest.AddListener(LoadSceneByIndex);
        uiTempLink.loadingSceneRequest.AddListener(LoadSceneByIndex);

    }
    #endregion

    #region Switch Scene Methods
    private void LoadSceneByIndex(int buildIndex)
    {
        if (!GameContN.playerDatasStatic.runtimeSceneCreationMode || buildIndex <= 2)
        {
            SceneManager.LoadScene(buildIndex);
        }
        else
        {
            Scene oldScene = SceneManager.GetActiveScene();
            Scene newScene = SceneManager.CreateScene(GameContN.playerDatasStatic.lastNewsVisited + GameContN.playerDatasStatic.lastSceneVisited);
            SceneManager.SetActiveScene(newScene);
            SceneManager.UnloadSceneAsync(oldScene);
            Instantiate(scenePrefab);
            GameObject psdToInstantiate = (GameObject)Instantiate(Resources.Load("viaosoppioxtool"));
            psdToInstantiate.transform.SetParent(GameObject.Find("GamePlay").transform);
            psdToInstantiate.transform.SetSiblingIndex(0);
            psdToInstantiate.GetComponent<RectTransform>().anchoredPosition = new Vector2(-800, 600);
            psdToInstantiate.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            foreach (var clue in psdToInstantiate.GetComponentsInChildren<Image>())
            {
                if (clue.transform.name != "bg")
                {
                    ClueCustomClickEvent newClue = clue.gameObject.AddComponent<ClueCustomClickEvent>();
                    clue.gameObject.AddComponent<Button>();
                }
                else
                {
                    //GameObject.Find("GamePlay").GetComponent<Image>().sprite = clue.sprite;
                    //Destroy(clue);
                }
            }
        }
    }

    private void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    } 
    #endregion
}
