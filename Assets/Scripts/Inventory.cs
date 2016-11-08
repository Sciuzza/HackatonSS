using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    GameObject prefab;
    public int n = 5;
    public List<GameObject> container;
    //public GameObject[] itemContainer;
    public List<GameObject> itemContainer;
    public ClickableHandler[] clickableContainer;
	// Use this for initialization
	void Start () {
        prefab = Resources.Load("SlotInvetario") as GameObject;
        container = new List<GameObject>();
        itemContainer = new List<GameObject>();
        clickableContainer = FindObjectsOfType<ClickableHandler>();
        
        for (int i = 0; i < clickableContainer.Length; i++)
        {
            GameObject prefabGO = Instantiate(prefab);
            container.Add(prefabGO);
            prefabGO.transform.position = new Vector3(-4+i, -4.5f, 0);
            
            
        }
        n = 0;
	}

    public void SetInventory(GameObject item)
    {
        itemContainer.Add(item);
        
        //item.transform.parent = container[n].transform;
        //item.transform.position = container[n].transform.position;
        n++;
        item.AddComponent<InventoryTooltip>();
        item.GetComponent<InventoryTooltip>().textTooltip = item.GetComponent<ClickableHandler>().textTooltip;
        Destroy(item.GetComponent<ClickableHandler>());

        if (itemContainer.Count == 1) {
            GameObject.FindGameObjectWithTag("LevelCom").GetComponent<ExitBeha>().EnablingExit();
            GameObject.FindGameObjectWithTag("Finish").GetComponent<Text>().enabled = true;
            }
    }
}
