using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameController : MonoBehaviour {


	public int[] VictimClues = new int[4];
	public int[] DetectClues = new int[4];

	private int currentSceneIndex;

	private bool isVictimStory = false;

	void Start () {
		
		Scene currentScene = SceneManager.GetActiveScene ();
		currentSceneIndex = currentScene.buildIndex;

		ExitBeha cmTempLink = GameObject.FindGameObjectWithTag("Exit").GetComponent<ExitBeha>();
		cmTempLink.levelFinished.AddListener(LevelCompleteManager);
	}

	private void LevelCompleteManager(int sceneToGo){

		//Prende il gestore dell'inventario e legge il numero degli indizi che sono stati trovati
		//Controlla se è la storia della vittima o del detective
		//Controlla quanti indizi massimi dovrebbe avere la scena corrente
		//A seconda della percentuale raggiunta fa partire un background piuttosto che un altro nella
		//scena di caricamento
		/*
		Inventory invTempLink = GameObject.FindGameObjectWithTag ("Inventory");
		int currentScore = invTempLink.array.length ();
        */
	}

}
