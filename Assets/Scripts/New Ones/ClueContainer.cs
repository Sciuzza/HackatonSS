using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClueContainer : MonoBehaviour
{
	sensibleClueData clueData;
	string clueText;
	private GameObject clueInfoPanel;


	void Inizialization()
	{
		ClueCustomClickEvent refEvent = GetComponent<ClueCustomClickEvent>();

		string tempClueName = refEvent.clueName;

		int templastScene = GameContN.Self.playerDatas.lastSceneVisited;
		string tempLastNews = GameContN.Self.playerDatas.lastNewsVisited;
		string tempLastCity = GameContN.Self.playerDatas.lastCityVisited;

		sensibleMapData tempMapData = GameContN.Self.playerDatas.mapData.Find(x => x.mapName == tempLastCity);

		sensibleNewsData tempNewsData = tempMapData.newsData.Find(x => x.newsName == tempLastNews);

		sensibleSceneData tempSceneData = tempNewsData.scenesData.Find(x => x.sceneIndex == templastScene);

		clueData = tempSceneData.cluesData.Find(x => x.clueName == tempClueName);

		clueText = clueData.clueInfoText;
		GetComponent<ClueCustomClickEvent>().customClick.AddListener(ClueInfoVisualizer);
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