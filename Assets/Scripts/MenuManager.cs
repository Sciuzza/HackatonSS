using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    private int sceneIndex;
    public Button prova;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        
    }

    

}
