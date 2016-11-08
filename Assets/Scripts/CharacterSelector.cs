using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour {

    public bool isClicked = false;
    public string chooseCharacter;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        if (!isClicked)
        {
            SceneManager.LoadScene(chooseCharacter);
        }
    }


}
