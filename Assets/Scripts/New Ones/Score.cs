using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    public Text[] textBoxArray = new Text[4];
    public Text textTotScore;
    public CustomClickEvent[] specificScenes = new CustomClickEvent[4];

    public event_int loadingSceneRequest;

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

            if (foundCluesInScene < GameContN.playerDatasStatic.mapData[0].newsData[0].scenesData[i].clueCounter)
            {
                specificScenes[i].gameObject.GetComponent<Image>().color = Color.red;
                specificScenes[i].customClick.AddListener(loadingSceneRequestMethod);
            }

        }
        textTotScore.text = totCluesFound + "/" + totCluesInNews;
	}


    private void loadingSceneRequestMethod(int buildIndex)
    {
        GameContN.Self.playerDatas.lastSceneVisited = buildIndex - 3;
        loadingSceneRequest.Invoke(buildIndex);
    }

}
