using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNewsRead : MonoBehaviour {

    public Image[] newsPieces = new Image[4];
    public Sprite[] failedSprites = new Sprite[4];

    public bool[] allFound = { true, true, true, true };

    void Awake()
    {
        for (int j = 0; j < GameContN.playerDatasStatic.mapData[0].newsData[0].sceneCounter; j++)
        {
            for (int i = 0; i < GameContN.playerDatasStatic.mapData[0].newsData[0].scenesData[0].clueCounter; i++)
            {
                if (!GameContN.playerDatasStatic.mapData[0].newsData[0].scenesData[j].cluesData[i].hasBeenFound)
                {
                    allFound[j] = false;
                    newsPieces[j].sprite = failedSprites[j];
                }
            }
        }
    }
}
