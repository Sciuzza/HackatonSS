using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitBeha : MonoBehaviour {

	public int SceneToGo;

	void OnMouseDown() {
		SceneManager.LoadScene (SceneToGo);
	}
}
