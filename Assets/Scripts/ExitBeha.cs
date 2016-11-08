using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ExitBeha : MonoBehaviour {



	public UnityEvent levelFinished;


    void Awake()
    {
        //this.GetComponent<BoxCollider2D>().enabled = false;
    }

	void OnMouseDown() {
		levelFinished.Invoke ();
	}
}
