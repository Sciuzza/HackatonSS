using UnityEngine;
using System.Collections;

public class Prova_fabri : MonoBehaviour {
    public GameObject prefab;
    public int n = 5;
    public GameObject[] container;
	// Use this for initialization
	void Start () {

        container = new GameObject[n];
        for (int i = 0; i < n; i++)
        {
            GameObject prefabGO = Instantiate(prefab);
            container[i] = prefabGO;
            prefabGO.transform.position = new Vector3(-2    +i, -4.5f, 0);
            
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        Debug.Log("Click");
    }
}
