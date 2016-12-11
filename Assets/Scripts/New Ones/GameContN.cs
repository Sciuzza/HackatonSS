using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections.Generic;

#region Event Classes
[System.Serializable]
public class event_int_bool_int : UnityEvent<int, bool, int>
{
}

[System.Serializable]
public class event_int : UnityEvent<int>
{
}

[System.Serializable]
public class event_cities : UnityEvent<string>
{
}

[System.Serializable]
public class event_string : UnityEvent<string>
{
}
#endregion

#region Sensible Data Structures

[System.Serializable]
public class sensibleGeneralData
{
    public int lastSceneVisited;
    public string lastNewsVisited;
    public string lastCityVisited;
    public List<sensibleMapData> mapData;
}

[System.Serializable]
public class sensibleMapData
{
    public string mapName;
    public List<sensibleNewsData> newsData;
}

[System.Serializable]
public class sensibleNewsData
{
    public string newsName;
    public string newsInfoText;
    public int playerCurrentScore;
    public List<sensibleSceneData> scenesData;
}

[System.Serializable]
public class sensibleSceneData
{
    public int sceneIndex;
    public List<sensibleClueData> cluesData;
}

[System.Serializable]
public class sensibleClueData
{
    public string clueName;
    public string clueInfoText;
    public bool hasBeenFound;
}
#endregion


public class GameContN : MonoBehaviour {

    // Singleton Implementation
    protected static GameContN _self;
    public static GameContN Self
    {
        get
        {
            if (_self == null)
                _self = FindObjectOfType(typeof(GameContN)) as GameContN;
            return _self;
        }
    }

    #region Public Variables
    [SerializeField]
    public sensibleGeneralData playerDatas;
    public static sensibleGeneralData playerDatasStatic;
    #endregion

    #region Private Variables

    #endregion

    #region Events
    public event_int loadingMenuRequest;
    public UnityEvent mmInitRequest, mapInitRequest, gameplayInitRequest, readingNewsRequest, scoreRequest, loadDataRequest, saveDataRequest;
    #endregion

    #region Do not Destroy Logic, Player Data Static Trick, Taking References and Linking Events
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        playerDatasStatic = playerDatas;

        UiContN uiTempLink = this.gameObject.GetComponent<UiContN>();

        uiTempLink.quitGame.AddListener(QuittinGame);

    }
    #endregion

    #region Initilization
    public void Initialization()
    {

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                loadDataRequest.Invoke();
                loadingMenuRequest.Invoke(1);
                break;
            case 1:
                mmInitRequest.Invoke();
                break;
            case 2:
                mapInitRequest.Invoke();
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                Debugging("GG");
                gameplayInitRequest.Invoke();
                break;
            case 7:
                readingNewsRequest.Invoke();
                break;
            case 8:
                scoreRequest.Invoke();
                break;
        }

    }

    private void QuittinGame()
    {
        Debugging("Quitting Game");
        Application.Quit();
    }
    #endregion

    #region General Methods
    void OnApplicationQuit()
    {
        //saveDataRequest.Invoke();
    }
    #endregion

    #region Debugging Static Methods
    public static void Debugging(string debugString)
    {
        //#if GG
        Debug.Log(debugString);
        //#endif
    }

    public static void Debugging(string debugString, int debugInt)
    {
        //#if GG
        Debug.Log(debugString + " " + debugInt);
        //#endif
    }
    #endregion

}
