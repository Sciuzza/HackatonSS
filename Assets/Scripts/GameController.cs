using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

#region Event Classes
[System.Serializable]
public class event_int_bool_int : UnityEvent<int, bool, int>
{
} 
#endregion

public class GameController : MonoBehaviour
{
    public int[] VictimClues = new int[4];
    public int[] DetectClues = new int[4];

    private int currentSceneIndex;
    private int nextScene;
	private int currentScore;
    private bool isVictimStory = false;
    private bool isOnGame = false;
    private bool isMaxScore = false;

    public event_int_bool_int loadingloaded;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;

        Debug.Log("Scena " + currentSceneIndex);

        SettingVariables(currentSceneIndex);

        if (currentScene.buildIndex >= 7)
        {

            ExitBeha cmTempLink = GameObject.FindGameObjectWithTag("LevelCom").GetComponent<ExitBeha>();

            if (cmTempLink == null)
                Debug.LogWarning("Missing Exit");

            cmTempLink.levelFinished.AddListener(LevelCompleteManager);
        }
        else if (currentScene.buildIndex == 4)
        {

            if (isMaxScore)
                Debug.Log("Max Score");
            else
                Debug.Log("Normal Score");
        }
        else if (currentSceneIndex == 0)
            SceneManager.LoadScene(1);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentSceneIndex <= 6 && currentSceneIndex > 1)
                SceneManager.LoadScene(1);
            else if (currentSceneIndex == 1)
                Application.Quit();
            else
                SceneManager.LoadScene(3);
        }

    }

    private void LevelCompleteManager()
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

    private void SettingVariables(int buildIndex)
    {
        if (buildIndex >= 7)
        {
            if (buildIndex <= 10)
                isVictimStory = true;
            else
                isVictimStory = false;

            isOnGame = true;
            isMaxScore = false;
        }
        else
            isOnGame = false;
    }

    public void SettingLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;

        Debug.Log("Scena " + currentSceneIndex);

        if (currentSceneIndex != 4)
        SettingVariables(currentSceneIndex);

        if (currentScene.buildIndex >= 7)
        {

            ExitBeha cmTempLink = GameObject.FindGameObjectWithTag("LevelCom").GetComponent<ExitBeha>();

            if (cmTempLink == null)
                Debug.LogWarning("Missing Exit");

            cmTempLink.levelFinished.AddListener(LevelCompleteManager);
        }
        else if (currentScene.buildIndex == 4)
        {

            if (isMaxScore)
                Debug.Log("Max Score");
            else
                Debug.Log("Normal Score");

            if ((isVictimStory && nextScene <= 10) || (!isVictimStory && nextScene >= 11 && nextScene <= 13))
				loadingloaded.Invoke(nextScene + 1, isMaxScore, currentScore);
            else
				loadingloaded.Invoke(5, isMaxScore, currentScore);
        }
        else if (currentSceneIndex == 0)
            SceneManager.LoadScene(1);
    }
}
