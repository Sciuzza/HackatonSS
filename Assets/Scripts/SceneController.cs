using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {


    void Awake()
    {
        InputController icTempLink = this.GetComponent<InputController>();

        icTempLink.escapeRequest.AddListener(EscapeHandler);
    }

    private void EscapeHandler()
    {
        if ( SceneManager.GetActiveScene().buildIndex <= 6 && SceneManager.GetActiveScene().buildIndex > 1)
            SceneManager.LoadScene(1);
        else if (SceneManager.GetActiveScene().buildIndex == 1)
            Application.Quit();
        else
            SceneManager.LoadScene(3);
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
