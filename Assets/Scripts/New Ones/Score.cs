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
    Image[] imageToFlash = new Image[4];
    RectTransform[] rectToScale = new RectTransform[4];

    float timer = 0;

    public bool[] hasToFlash = new bool[4];

    public event_int loadingSceneRequest;

	void Start ()
    {
        int totCluesFound = 0;
        int totCluesInNews = 0;

        for (int i = 0; i < 4; i++)
        {
            imageToFlash[i] = specificScenes[i].GetComponent<Image>();
            rectToScale[i] = specificScenes[i].GetComponent<RectTransform>();
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
                hasToFlash[i] = true;
                specificScenes[i].customClick.AddListener(loadingSceneRequestMethod);
                imageToFlash[i].color = Color.red;
            }
        }
        textTotScore.text = totCluesFound + "/" + totCluesInNews;
	}


    void Update()
    {
        timer += Time.deltaTime;

        for (int i = 0; i < 4; i++)
        {
            if (hasToFlash[i])
            {
                rectToScale[i].localScale = Vector2.Lerp(new Vector3(0.8f, 0.8f), new Vector3(1.1f, 1.1f), Mathf.Abs(Mathf.Sin(timer * 3)));
            }
        }
    }

    private void loadingSceneRequestMethod(int buildIndex)
    {
        GameContN.Self.playerDatas.lastSceneVisited = buildIndex - 3;
        loadingSceneRequest.Invoke(buildIndex);
    }

}
