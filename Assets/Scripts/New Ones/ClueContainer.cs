using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClueContainer : MonoBehaviour
{
    [SerializeField]
	public sensibleClueData clueData;
	public string clueText;
	private GameObject clueInfoPanel;


	public void Initialization()
	{
		ClueCustomClickEvent refEvent = GetComponent<ClueCustomClickEvent>();

        string tempClueName = this.name;
		int templastScene = GameContN.Self.playerDatas.lastSceneVisited;        
		string tempLastNews = GameContN.Self.playerDatas.lastNewsVisited;        
        string tempLastCity = GameContN.Self.playerDatas.lastCityVisited;
        

        sensibleMapData tempMapData = GameContN.Self.playerDatas.mapData.Find(x => x.mapName == tempLastCity);

        

		sensibleNewsData tempNewsData = tempMapData.newsData.Find(x => x.newsName == tempLastNews);
        
        sensibleSceneData tempSceneData = tempNewsData.scenesData.Find(x => x.sceneIndex == templastScene);
        
        clueData = tempSceneData.cluesData.Find(x => x.clueName == tempClueName);
        
        clueText = clueData.clueInfoText;
        //refEvent.clueInfoText = clueText;
		//GetComponent<ClueCustomClickEvent>().customClick.AddListener(ClueInfoVisualizer);
	}

	void ClueInitializer()
	{
		
		clueInfoPanel = GameObject.Find("ClueInfo");
		clueInfoPanel.SetActive(false);

	}

	void ClueInfoVisualizer(string infoToVisualize)
	{
		clueInfoPanel.SetActive(true);
	}
}