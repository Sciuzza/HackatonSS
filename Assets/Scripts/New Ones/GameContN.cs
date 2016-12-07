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
#endregion

#region Sensible Data Structures
[System.Serializable]
public struct sensibleData
{
    public int sceneIndex;
}
#endregion


public class GameContN : MonoBehaviour {

    #region Public Variables

    #endregion

    #region Private Variables
    private sensibleData Datas;
    #endregion

    #region Events
    public event_int loadingMenuRequest;
    public UnityEvent mmInitRequest;
    #endregion

    #region Do not Destroy Logic
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        UiContN uiTempLink = this.gameObject.GetComponent<UiContN>();

        uiTempLink.quitGame.AddListener(QuittinGame);

    }
    #endregion

    #region Initilization
    public void Initialization(int buildIndex)
    {
        Datas.sceneIndex = buildIndex;

        switch (Datas.sceneIndex)
        {
            case 0:
                loadingMenuRequest.Invoke(buildIndex + 1);
                break;
            case 1:
                mmInitRequest.Invoke();
                break;
            case 2:
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
