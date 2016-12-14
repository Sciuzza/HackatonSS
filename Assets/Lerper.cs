using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lerper : MonoBehaviour
{ 
    float timer = 0;
	
	void Update ()
    {
        timer += Time.deltaTime;

        GetComponent<RectTransform>().localScale = Vector2.Lerp(new Vector3(0.8f, 0.8f), new Vector3(1.1f, 1.1f), Mathf.Abs(Mathf.Sin(timer * 3)));
    }
}
