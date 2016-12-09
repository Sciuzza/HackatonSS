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
public class event_cities : UnityEvent<cities>
{
}

[System.Serializable]
public class event_string : UnityEvent<string>
{
}
#endregion

#region Sensible Data Structures

public enum cities { Milan, Rome};

[System.Serializable]
public struct sensibleGeneralData
{
    public int lastSceneVisited;
    public int lastNewsVisited;
    public cities lastCityVisited;
    public sensibleMapData[] mapData;
}

[System.Serializable]
public struct sensibleMapData
{
    public cities mapName;
    public List<sensibleNewsData> newsData;
}

[System.Serializable]
public struct sensibleNewsData
{
    public int newsIndex;
    public string newsInfoText;
    public int playerCurrentScore;
    public sensibleSceneData[] scenesData;
}

[System.Serializable]
public struct sensibleSceneData
{
    public int sceneIndex;
    public sensibleClueData[] cluesData;
}

[System.Serializable]
public struct sensibleClueData
{
    public string clueName;
    public string clueInfoText;
    public bool hasBeenFound;
}
#endregion


public class GameContN : MonoBehaviour {

    #region Public Variables
    [SerializeField]
    public sensibleGeneralData playerDatas;
    public static sensibleGeneralData playerDatasStatic;
    #endregion

    #region Private Variables

    #endregion

    #region Events
    public event_int loadingMenuRequest;
    public UnityEvent mmInitRequest, mapInitRequest;
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
    public void Initialization(int buildIndex)
    {
        playerDatas.lastSceneVisited = buildIndex;

        switch (playerDatas.lastSceneVisited)
        {
            case 0:
                loadingMenuRequest.Invoke(buildIndex + 1);
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
                break;
            case 7:
                break;
            case 8:
                break;
        }

    }

    private void QuittinGame()
    {
        Debugging("Ciccio");
        Application.Quit();
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
