using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ExitBeha : MonoBehaviour {



	public UnityEvent levelFinished;



	void OnMouseDown() {
		levelFinished.Invoke ();
	}
}
