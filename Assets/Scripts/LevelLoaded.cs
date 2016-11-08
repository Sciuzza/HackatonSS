using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoaded : MonoBehaviour {

	void Start()
    {
        GameObject gcTempLink = GameObject.FindGameObjectWithTag("GameController");

        if (gcTempLink == null)
            Debug.Log("Not Found");
      

        gcTempLink.GetComponent<GameController>().SettingLevel();
    }

   

   
}
