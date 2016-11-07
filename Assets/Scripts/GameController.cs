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


	[System.Serializable]
	public class loadingEvent : UnityEvent<int, bool>
	{
	}

	public loadingEvent loadingloaded;

	void Start () {

		Scene currentScene = SceneManager.GetActiveScene ();
		currentSceneIndex = currentScene.buildIndex;

		SettingVariables (currentSceneIndex);

		if (currentScene.buildIndex >= 7) {
			
			ExitBeha cmTempLink = GameObject.FindGameObjectWithTag ("LevelCom").GetComponent<ExitBeha> ();

			if (cmTempLink == null)
				Debug.LogWarning ("Missing Exit");

			cmTempLink.levelFinished.AddListener (LevelCompleteManager);
		} 
		else if (currentScene.buildIndex == 4) {


			if (isMaxScore)
				Debug.Log("Max Score");
			else
				Debug.Log("Normal Score");
		}
	}


	void Update(){

		if (Input.GetKeyDown(KeyCode.Escape)){

			if (currentSceneIndex <=6 && currentSceneIndex > 1)
				SceneManager.LoadScene(1);
			else if (currentSceneIndex == 1)
				Application.Quit();
			else
				SceneManager.LoadScene(3);

		}

	}

	private void LevelCompleteManager(){



		Inventory invTempLink = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory>();
		int currentScore = invTempLink.itemContainer.Count;
        

		int maxScore;

		if (isVictimStory) {
			maxScore = VictimClues [currentSceneIndex - 7];
		} 
		else {
			maxScore = DetectClues [currentSceneIndex - 11];
		}

		if (currentScore == maxScore)
			isMaxScore = true;
		else
			isMaxScore = false;

		int nextscene = currentSceneIndex;

		SceneManager.LoadScene (4);

		if ((isVictimStory && nextscene <= 9) ||(!isVictimStory && nextscene >= 11 && nextscene <= 13))
		    loadingloaded.Invoke (nextscene + 1, isMaxScore);
		else
			loadingloaded.Invoke (5, isMaxScore);

	}

	private void SettingVariables(int buildIndex){

		if (buildIndex >= 7) {

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
}
