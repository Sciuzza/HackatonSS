using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour {

	public int nextIndex;


	void OnMouseDown(){
		SceneManager.LoadScene (nextIndex);
	}
}
