using UnityEngine;
using System.Collections;

public class loadScr : MonoBehaviour {

	private GameObject maxScoreB, avgScoreB;

	// Use this for initialization
	void Awake () {
	
		GameController gcTempLink = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		gcTempLink.loadingloaded.AddListener (SettingLoadingScreen);

		maxScoreB = GameObject.FindGameObjectWithTag ("MaxScore");
		avgScoreB = GameObject.FindGameObjectWithTag ("AvgScore");

		maxScoreB.SetActive(false);
		avgScoreB.SetActive(false);

	}


	private void SettingLoadingScreen(int nextScene, bool maxScore){

		GameObject.FindGameObjectWithTag ("NextScene").GetComponent<Next> ().nextIndex = nextScene;
		GameObject.FindGameObjectWithTag ("PreviousScene").GetComponent<Previous> ().previousIndex = nextScene - 1;

		if (maxScore)
			maxScoreB.SetActive(true);
		else
			avgScoreB.SetActive(true);
	}

}
