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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
       
        }
    }
    #endregion

    #region Switch Scene Methods
    private void LoadSceneByIndex(int buildIndex)
    {
        if (!GameContN.playerDatasStatic.runtimeSceneCreationMode)
        {
            SceneManager.LoadScene(buildIndex);
        }
        else if(GameContN.playerDatasStatic.runtimeSceneCreationMode && (buildIndex > 2 || SceneManager.GetActiveScene().buildIndex == -1 ))
        {
            Scene oldScene = SceneManager.GetActiveScene();
            Scene newScene = SceneManager.CreateScene(GameContN.playerDatasStatic.lastNewsVisited + GameContN.playerDatasStatic.lastSceneVisited);
            SceneManager.SetActiveScene(newScene);
            SceneManager.UnloadSceneAsync(oldScene);
            Instantiate(scenePrefab);
            Debug.Log(GameContN.playerDatasStatic.lastNewsVisited + GameContN.playerDatasStatic.lastSceneVisited);
            GameObject psdToInstantiate = (GameObject)Instantiate(Resources.Load(GameContN.playerDatasStatic.lastNewsVisited+GameContN.playerDatasStatic.lastSceneVisited));
            psdToInstantiate.transform.SetParent(GameObject.Find("GamePlay").transform);
            psdToInstantiate.transform.SetSiblingIndex(0);
            psdToInstantiate.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1024, 768);
            psdToInstantiate.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            foreach (var clue in psdToInstantiate.GetComponentsInChildren<Image>())
            {
                if (clue.transform.name != "bg")
                {
                    ClueCustomClickEvent newClue = clue.gameObject.AddComponent<ClueCustomClickEvent>();
                    newClue.clueInfoText = GameContN.playerDatasStatic.mapData.Find(x => x.mapName == GameContN.playerDatasStatic.lastCityVisited).newsData.Find(x => x.newsName == GameContN.playerDatasStatic.lastNewsVisited).scenesData.Find(x => x.sceneIndex == GameContN.playerDatasStatic.lastSceneVisited).cluesData.Find(x => x.clueName == newClue.transform.name).clueInfoText;
                    //newClue.customClick.AddListener(Clue);
                    clue.gameObject.AddComponent<Button>();
                    ClueContainer ccTemp = clue.gameObject.AddComponent<ClueContainer>();
                }                 
            }
            FindObjectOfType<GameContN>().gameplayInitRequest.Invoke();
        }
        else
        {
            SceneManager.LoadScene(buildIndex);
        }
    }

    private void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    } 
    #endregion
}
