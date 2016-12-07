
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections.Generic;

/*
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
*/
public class GameController : MonoBehaviour
{
    #region Public Variables
    public List<NewsSelector> newsList = new List<NewsSelector>();
    public int[] VictimClues = new int[4];
    public int[] DetectClues = new int[4]; 
    #endregion

    #region Private Variables
    private int currentSceneIndex;
    private int nextScene;
    private int currentScore;
    private bool isVictimStory = false;
    private bool isOnGame = false;
    private bool isMaxScore = false; 
    #endregion

    #region Events
    public event_int_bool_int loadingloaded; 
    #endregion

    #region Do not Destroy Logic
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region Initialization Methods
    public void Initialization()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;

        Debugging("Scene", currentSceneIndex);


        if (currentScene.buildIndex >= 7)
            GameplayInitialization();
        else if (currentScene.buildIndex == 4)
            LoadingScreenInitialization();
        else if (currentSceneIndex == 0)
            SceneManager.LoadScene(1);
    }

    private void GameplayInitialization()
    {
        ExitBeha cmTempLink = GameObject.FindGameObjectWithTag("LevelCom").GetComponent<ExitBeha>();

        if (cmTempLink == null)
            Debug.LogWarning("Missing Exit");

        cmTempLink.levelFinished.AddListener(GameplayLevelCompleteHandler);


        if (SceneManager.GetActiveScene().buildIndex <= 10)
            isVictimStory = true;
        else
            isVictimStory = false;

        isOnGame = true;
        isMaxScore = false;
    }

    private void NoGameplayInitialization()
    {
        isOnGame = false;
    }

    private void LoadingScreenInitialization()
    {
        if (isMaxScore)
            Debug.Log("Max Score");
        else
            Debug.Log("Normal Score");

        isOnGame = false;
    }
    #endregion

    #region End Level Methods
    private void GameplayLevelCompleteHandler()
    {
        Inventory invTempLink = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        currentScore = invTempLink.itemContainer.Count;


        int maxScore;

        if (isVictimStory)
        {
            maxScore = VictimClues[currentSceneIndex - 7];
        }
        else
        {
            maxScore = DetectClues[currentSceneIndex - 11];
        }

        if (currentScore == maxScore)
            isMaxScore = true;
        else
            isMaxScore = false;

        nextScene = currentSceneIndex;

        SceneManager.LoadScene(4);
    } 
    #endregion

    #region Debugging Static Methods
    private static void Debugging(string debugString)
    {
        //#if GG
        Debug.Log(debugString);
        //#endif
    }

    private static void Debugging(string debugString, int debugInt)
    {
        //#if GG
        Debug.Log(debugString + " " + debugInt);
        //#endif
    } 
    #endregion
}
