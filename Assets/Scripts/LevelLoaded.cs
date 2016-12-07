using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoaded : MonoBehaviour
{
	void Start()
    {
        GameObject gcTempLink = FindObjectOfType<GameController>().gameObject;

        if (gcTempLink == null)
            Debug.Log("Not Found");
      
        gcTempLink.GetComponent<GameController>().Initialization();
    }   
}
