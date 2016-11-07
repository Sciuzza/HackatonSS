using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ExitBeha : MonoBehaviour {

	public int SceneToGo;



	[System.Serializable]
	public class MyIntEvent : UnityEvent<int>
	{
	}

	public MyIntEvent levelFinished;



	void OnMouseDown() {
		levelFinished.Invoke (SceneToGo);
	}
}
