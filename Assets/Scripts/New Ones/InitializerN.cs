using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InitializerN : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameObject gcTempLink = GameObject.FindGameObjectWithTag("GameController");

        if (gcTempLink == null)
            GameContN.Debugging("Brain Not Found");

        gcTempLink.GetComponent<GameContN>().Initialization(SceneManager.GetActiveScene().buildIndex);

    }
	
	
}
