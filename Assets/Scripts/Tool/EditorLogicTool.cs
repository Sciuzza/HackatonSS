using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

//[ExecuteInEditMode]
public class EditorLogicTool : MonoBehaviour
{
    public List<string> textsToImport;

    void Start()
    {
        SceneCreator scRef = FindObjectOfType<SceneCreator>();
        string path = Application.persistentDataPath + "/scene" + scRef.currentScene+ ".txt";
        StreamReader sr = File.OpenText(path);
        textsToImport = new List<string>();
        string s;
        while ((s = sr.ReadLine()) != null)
        {
            textsToImport.Add(s);
        }
        sr.Close();

        if (GameObject.Find("bg"))
        {
           
        }
        if (GameObject.Find("clue1"))
        {
            if (!GameObject.Find("clue1").GetComponent<BoxCollider2D>())
            {
                GameObject.Find("clue1").AddComponent<BoxCollider2D>();
            }
            if (!GameObject.Find("clue1").GetComponent<ClickableHandler>())
            {
                ClickableHandler chRef = GameObject.Find("clue1").AddComponent<ClickableHandler>();
                chRef.textTooltip = textsToImport[0];
            }
        }
        if (GameObject.Find("clue2"))
        {
            if (!GameObject.Find("clue2").GetComponent<BoxCollider2D>())
            {
                GameObject.Find("clue2").AddComponent<BoxCollider2D>();
            }
            if (!GameObject.Find("clue2").GetComponent<ClickableHandler>())
            {
                ClickableHandler chRef = GameObject.Find("clue2").AddComponent<ClickableHandler>();
                chRef.textTooltip = textsToImport[1];
            }
        }
        if (GameObject.Find("clue3"))
        {
            if (!GameObject.Find("clue3").GetComponent<BoxCollider2D>())
            {
                GameObject.Find("clue3").AddComponent<BoxCollider2D>();
            }
            if (!GameObject.Find("clue3").GetComponent<ClickableHandler>())
            {
                ClickableHandler chRef = GameObject.Find("clue3").AddComponent<ClickableHandler>();
                chRef.textTooltip = textsToImport[2];
            }
        }
        if (GameObject.Find("clue4"))
        {
            if (!GameObject.Find("clue4").GetComponent<BoxCollider2D>())
            {
                GameObject.Find("clue4").AddComponent<BoxCollider2D>();
            }
            if (!GameObject.Find("clue4").GetComponent<ClickableHandler>())
            {
                ClickableHandler chRef = GameObject.Find("clue4").AddComponent<ClickableHandler>();
                chRef.textTooltip = textsToImport[3];
            }
        }
        if (GameObject.Find("clue5"))
        {
            if (!GameObject.Find("clue5").GetComponent<BoxCollider2D>())
            {
                GameObject.Find("clue5").AddComponent<BoxCollider2D>();
            }
            if (!GameObject.Find("clue5").GetComponent<ClickableHandler>())
            {
                ClickableHandler chRef = GameObject.Find("clue5").AddComponent<ClickableHandler>();
                chRef.textTooltip = textsToImport[4];
            }
        }
    }
}
