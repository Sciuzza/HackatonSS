using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneCreator : MonoBehaviour
{
    GameContN gcRef;
    EditorLogicTool edRef;
    List<int> projectValues;
    GameObject logicPrefab, uiPrefab;

    void Start()
    {
        edRef = FindObjectOfType<EditorLogicTool>();
        gcRef = FindObjectOfType<GameContN>();
        edRef.InitializeScene();
        string path = Application.persistentDataPath + "/scene" + gcRef.playerDatas.lastSceneVisited + ".txt";
        StreamReader sr = File.OpenText(path);
        projectValues = new List<int>();
        int s;
        while (sr.ReadLine() != null)
        {
            s = int.Parse(sr.ReadLine());
            projectValues.Add(s);
        }
        sr.Close();

        if (gcRef.playerDatas.lastSceneVisited < projectValues[0])
        {
            SceneManager.CreateScene("Scene" + gcRef.playerDatas.lastSceneVisited);
            Instantiate(Resources.Load("PsdScene" + gcRef.playerDatas.lastSceneVisited + ".psd"));
            Instantiate(uiPrefab);
            Instantiate(logicPrefab);
        }
        else
        {
            //goto finale;
        }

    }
}
