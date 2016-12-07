using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class loadScr : MonoBehaviour {

	private int nextSceneSaved;

	void Awake ()
    {	
		GameController gcTempLink = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();

		if (gcTempLink == null)
			Debug.LogWarning ("gc not found");
		gcTempLink.loadingloaded.AddListener(SettingLoadingScreen);
	}


	private void SettingLoadingScreen(int nextScene, bool maxScore, int currentScore)
    {
		nextSceneSaved = nextScene;

		if (maxScore)
        {
			GameObject.FindGameObjectWithTag ("Score").GetComponent<Text> ().text = "Congratulations, you have found all the clues";
		}
		else
        {
			GameObject.FindGameObjectWithTag ("Score").GetComponent<Text> ().text = "You have found " + currentScore +   " clues";
		}	
		GameObject.FindGameObjectWithTag ("NextScene").GetComponent<Button> ().onClick.AddListener(SettingSceneIndex);
		GameObject.FindGameObjectWithTag ("PreviousScene").GetComponent<Button> ().onClick.AddListener(SettingPreviousSceneIndex);
	}

	private void SettingSceneIndex()
    {
		SceneManager.LoadScene (nextSceneSaved);
	}

	private void SettingPreviousSceneIndex()
    {
		SceneManager.LoadScene (nextSceneSaved - 1);
	}
}
