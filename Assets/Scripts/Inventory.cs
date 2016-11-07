using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {
    public UnityEvent takeEvent;
    GameObject prefab;
    public int n = 5;
    public GameObject[] container;
	// Use this for initialization
	void Start () {
        prefab = Resources.Load("SlotInvetario") as GameObject;
        container = new GameObject[n];
        for (int i = 0; i < n; i++)
        {
            GameObject prefabGO = Instantiate(prefab);
            //container[i] = prefabGO;
            prefabGO.transform.position = new Vector3(-2+i, -4.5f, 0);
            
            
        }
	}


    void OnMouseUp()
    {
        Debug.Log("Click");
    }

    public void SetInventory(GameObject item)
    {
        
    }
}
