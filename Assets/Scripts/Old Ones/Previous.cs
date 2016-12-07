using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Previous : MonoBehaviour {

	public int previousIndex;


	void OnMouseDown(){
		SceneManager.LoadScene (previousIndex);
	}
}
