using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    GameObject prefab;
    public int n = 5;
    public GameObject[] container;
    //public GameObject[] itemContainer;
    public List<GameObject> itemContainer;
	// Use this for initialization
	void Start () {
        prefab = Resources.Load("SlotInvetario") as GameObject;
        container = new GameObject[n];
        itemContainer = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            GameObject prefabGO = Instantiate(prefab);
            container[i] = prefabGO;
            prefabGO.transform.position = new Vector3(-2+i, -4.5f, 0);
            
            
        }
        n = 0;
	}


    void OnMouseUp()
    {
        Debug.Log("Click");
    }

    public void SetInventory(GameObject item)
    {
        itemContainer.Add(item);
        item.transform.parent = container[n].transform;
        item.transform.position = container[n].transform.position;
        n++;
    }
}
