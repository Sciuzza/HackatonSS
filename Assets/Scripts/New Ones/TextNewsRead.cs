using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNewsRead : MonoBehaviour {

    public Image[] newsCorrectPieces = new Image[4];
    public Image[] newsWrongPieces = new Image[4];

    void Awake()
    {
        for (int j = 0; j < GameContN.playerDatasStatic.mapData[0].newsData[0].sceneCounter; j++)
        {
            for (int i = 0; i < GameContN.playerDatasStatic.mapData[0].newsData[0].scenesData[0].clueCounter; i++)
            {
                if (!GameContN.playerDatasStatic.mapData[0].newsData[0].scenesData[j].cluesData[i].hasBeenFound)
                {
                    newsCorrectPieces[j].color = Color.clear;
                    newsWrongPieces[j].color = Color.white;
                    break;                 
                }
                else
                {
                    newsWrongPieces[j].color = Color.clear;
                    newsCorrectPieces[j].color = Color.white;
                }   
            }
        }
    }
    public void OpenArchive()
    {
        Application.OpenURL("http://archivio.corriere.it/Archivio/interface/slider.html#!delitto-della-cattolica/NobwRAdghgtgpmAXGA1nAngdwPYCcAmYANGAC5wAepSY+cANgJamnYAEd99UbAxlC2xN+YAL4BdIA");
    }
}
