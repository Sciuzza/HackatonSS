using UnityEngine;
using System.Collections;


public class ClickableHandler : MonoBehaviour {

    Inventory refInv;
    
    
    void Start()
    {
        refInv = GetComponent<Inventory>();
    }
	void OnMouseUp()
    {
        refInv.SetInventory(this.gameObject);
    }
    
}
