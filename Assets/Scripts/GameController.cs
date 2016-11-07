using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameController : MonoBehaviour {


	public int[] VictimClues = new int[4];
	public int[] DetectClues = new int[4];

	private int currentSceneIndex;

	private bool isVictimStory = false;
	private bool isOnGame = false;
	private bool isMaxScore = false;

	void Start () {

		Scene currentScene = SceneManager.GetActiveScene ();
		currentSceneIndex = currentScene.buildIndex;

		SettingVariables (currentSceneIndex);

		if (currentScene.buildIndex >= 6) {
			
			ExitBeha cmTempLink = GameObject.FindGameObjectWithTag ("LevelCom").GetComponent<ExitBeha> ();

			if (cmTempLink == null)
				Debug.LogWarning ("Missing Exit");

			cmTempLink.levelFinished.AddListener (LevelCompleteManager);
		} 
		else if (currentScene.buildIndex == 3) {


			if (isMaxScore)
				Debug.Log("Max Score");
			else
				Debug.Log("Normal Score");
		}
	}

	private void LevelCompleteManager(int sceneToGo){



		Inventory invTempLink = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory>();
		int currentScore = invTempLink.itemContainer.Count;
        

		int maxScore;

		if (isVictimStory) {
			maxScore = VictimClues [sceneToGo - 6];
		} 
		else {
			maxScore = DetectClues [sceneToGo - 10];
		}

		if (currentScore == maxScore)
			isMaxScore = true;
		else
			isMaxScore = false;

		SceneManager.LoadScene (sceneToGo);

	}

	private void SettingVariables(int buildIndex){

		if (buildIndex >= 6) {

			if (buildIndex <= 9)
				isVictimStory = true;
			else
				isVictimStory = false;

			isOnGame = true;
			isMaxScore = false;


		}
		else
			isOnGame = false;
			
	}
}
