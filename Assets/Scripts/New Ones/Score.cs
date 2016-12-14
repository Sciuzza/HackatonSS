using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text[] textBoxArray = new Text[4];
    public Text textTotScore;

	void Start ()
    {
        int totCluesFound = 0;
        int totCluesInNews = 0;

        for (int i = 0; i < 4; i++)
        {
            int foundCluesInScene = 0;
            foreach (var clue in GameContN.playerDatasStatic.mapData[0].newsData[0].scenesData[i].cluesData)
            {
                totCluesInNews++;
                if (clue.hasBeenFound)
                {
                    foundCluesInScene++;
                    totCluesFound++;
                }
            }
            textBoxArray[i].text = foundCluesInScene + "/" + GameContN.playerDatasStatic.mapData[0].newsData[0].scenesData[i].clueCounter;
        }
        textTotScore.text = totCluesFound + "/" + totCluesInNews;
	}
}
