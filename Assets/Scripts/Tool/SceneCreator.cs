using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneCreator : MonoBehaviour
{
    public int currentScene = 0;
    List<int> projectValues;
    GameObject logicPrefab, uiPrefab;

	void Awake ()
    {
        string path = Application.persistentDataPath + "/scene.txt";
        StreamReader sr = File.OpenText(path);
        projectValues = new List<int>();
        int s;
        while (sr.ReadLine() != null)
        {
            s = int.Parse(sr.ReadLine());
            projectValues.Add(s);
        }
        sr.Close();

        if (currentScene<projectValues[0])
        {
            SceneManager.CreateScene("Scene" + currentScene + 1);
            Instantiate(logicPrefab);
            Instantiate(Resources.Load(currentScene + ".psd"));
            Instantiate(uiPrefab);

        }
        else
        {
            //goto finale;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
