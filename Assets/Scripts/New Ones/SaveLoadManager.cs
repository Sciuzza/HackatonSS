using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveLoadManager : MonoBehaviour
{

    public string path;

    #region Taking References and Linking Events
    void Awake()
    {
        path = Application.persistentDataPath + "/";
        GameContN gcTempLink = this.gameObject.GetComponent<GameContN>();

        if (gcTempLink.playerDatas.runtimeSceneCreationMode)
        {
            gcTempLink.loadDataRequest.AddListener(LoadingDataFromCsv);
            gcTempLink.saveDataRequest.AddListener(SavingDataOnCsv);
        }
    }
    #endregion

    private void LoadingDataFromCsv()
    {
        foreach (var file in Directory.GetFiles(Application.dataPath + "/CsvDatabase/"))
        {
            if (!file.Contains(".meta"))
            {
                if (!File.Exists(Application.persistentDataPath + "/" + file.Split('/')[file.Split('/').GetLength(0) - 1]))
                {
                    File.Copy(file, Application.persistentDataPath + "/" + file.Split('/')[file.Split('/').GetLength(0) - 1]);
                }
            }
        }

        //rename cristiano culo
        List<string[]> csvSaveFile = new List<string[]>();
        List<string[]> csvMapFile = new List<string[]>();
        List<string[]> tempCsvMapFile = new List<string[]>();
        List<string[]> tempCsvNewsFile = new List<string[]>();
        List<string[]> tempCsvSceneFile = new List<string[]>();
        List<List<string[]>> mapToNewsList = new List<List<string[]>>();
        List<List<string[]>> mapToNewsList2 = new List<List<string[]>>();
        List<List<string[]>> newsToSceneList = new List<List<string[]>>();
        List<List<List<string[]>>> mapToNewsToSceneList = new List<List<List<string[]>>>();
        List<List<List<List<string[]>>>> mapToNewsToSceneToClueList = new List<List<List<List<string[]>>>>();

        ReadCsv(path + "saveFile.csv", out csvSaveFile);
        //load saveFile
        GameContN.Self.playerDatas.mapData = new List<sensibleMapData>();
        GameContN.Self.playerDatas.lastCityVisited = csvSaveFile[0][0];
        GameContN.Self.playerDatas.lastNewsVisited = csvSaveFile[0][1];
        GameContN.Self.playerDatas.lastSceneVisited = int.Parse(csvSaveFile[0][2]);

        //load and create cities
        ReadCsv(path + "maps.csv", out csvMapFile);

        sensibleMapData newCity;
        for (int i = 0; i < csvMapFile.Count; i++)
        {
            newCity = new sensibleMapData();

            newCity.mapName = csvMapFile[i][0];
            newCity.newsData = new List<sensibleNewsData>();
            GameContN.Self.playerDatas.mapData.Add(newCity);

            //load and create news
            ReadCsv(path + csvMapFile[i][0] + ".csv", out tempCsvMapFile);
            mapToNewsList.Add(tempCsvMapFile);
        } 

        sensibleNewsData newNews;
        for (int i = 0; i < mapToNewsList.Count; i++)
        {
            for (int j = 0; j < mapToNewsList[i].Count; j++)
            {    
                newNews = new sensibleNewsData();
                newNews.scenesData = new List<sensibleSceneData>();

                newNews.newsName = mapToNewsList[i][j][0];
                newNews.newsInfoText = mapToNewsList[i][j][1];
                newNews.playerCurrentScore = int.Parse(mapToNewsList[i][j][2]);

                ReadCsv(path + mapToNewsList[i][j][0] + ".csv", out tempCsvNewsFile);
                newNews.sceneCounter = int.Parse(tempCsvNewsFile[tempCsvNewsFile.Count - 1][0]);

                GameContN.playerDatasStatic.mapData[i].newsData.Add(newNews);
                //load and create scenes
                newsToSceneList.Add(tempCsvNewsFile);
            }
        }

        sensibleSceneData newScene;
        for (int i = 0; i < mapToNewsList.Count; i++)
        {
            for (int j = 0; j < mapToNewsList[i].Count; j++)
            {
                for (int k = 0; k < GameContN.playerDatasStatic.mapData[i].newsData[j].sceneCounter; k++)
                {               
                    newScene = new sensibleSceneData();
                    newScene.cluesData = new List<sensibleClueData>();

                    newScene.sceneIndex = int.Parse(newsToSceneList[j][k][0]);

                    GameContN.playerDatasStatic.mapData[i].newsData[j].scenesData.Add(newScene);
                    //load and create clues
                    Debug.Log(newsToSceneList[j][k][0]);
                    ReadCsv(path + GameContN.playerDatasStatic.mapData[i].newsData[j].newsName + (k + 1) + ".csv", out tempCsvSceneFile);
                    mapToNewsList2.Add(tempCsvSceneFile);
                    mapToNewsToSceneList.Add(mapToNewsList2);
                    mapToNewsToSceneToClueList.Add(mapToNewsToSceneList);        
                }   
            }
        } 

        sensibleClueData newClue;
        for (int i = 0; i < mapToNewsList.Count; i++)
        {
            for (int j = 0; j < mapToNewsList[i].Count; j++)
            {
                for (int k = 0; k < GameContN.playerDatasStatic.mapData[i].newsData[j].sceneCounter; k++)
                {
                    for (int h = 0; h < mapToNewsToSceneToClueList[i][j][k].Count; h++)
                    {
                        newClue = new sensibleClueData();

                        Debug.Log(i.ToString()+ j.ToString() + k.ToString() + h.ToString());
                        Debug.Log(mapToNewsToSceneToClueList[i][j][k][h][0]);
                        Debug.Log(mapToNewsToSceneToClueList[i][j][k][h][1]);
                        Debug.Log(mapToNewsToSceneToClueList[i][j][k][h][2]);
                        Debug.Log(mapToNewsToSceneToClueList[i][j][k][h].Length);
                        newClue.clueName = mapToNewsToSceneToClueList[i][j][k][h][0];
                        newClue.clueInfoText = mapToNewsToSceneToClueList[i][j][k][h][1];
                        if (mapToNewsToSceneToClueList[i][j][k][h][2] == "true")
                        {
                            newClue.hasBeenFound = true;
                        }
                        else
                        {
                            newClue.hasBeenFound = false;

                        }
                        GameContN.playerDatasStatic.mapData[i].newsData[j].scenesData[k].cluesData.Add(newClue);
                    }
                }
            }
        }
        GameContN.Debugging("Data Loaded");
    }

    private void ReadCsv(string _fileName, out List<string[]> readOutPut)
    {
 
        readOutPut = new List<string[]>();
        char[] separator = { '*' };

        int counter = 0;
        StreamReader sr = File.OpenText(_fileName);
        string s;
        while ((s = sr.ReadLine()) != null)
        {
            if (counter == 0)
            {
                counter++;
                continue;
            }
            readOutPut.Add(s.Split(separator));
        }
        sr.Close();
    }

    private void WriteCsv(string _fileName, List<string[]> writeInPut)
    {
        Debug.LogError(writeInPut.Count);
        char[] separator = { '*' };

        int counter = 0;
        StreamWriter sw = File.CreateText(_fileName);
        string s = "";
        while (writeInPut.Count > 0)
        {

            if (counter == 0)
            {
                counter++;
            }
            else
            {
                for (int i = 0; i < writeInPut.Count; i++)
                {
                    for (int j = 0; j < writeInPut[i].Length; j++)
                    {
                        if (j < writeInPut[i].Length - 1)
                        {
                            s += writeInPut[i][j] + '*';
                        }
                        else
                        {
                            s += writeInPut[i][j];
                        }
                    }
                    sw.WriteLine(s);
                    Debug.Log(s);
                    writeInPut.RemoveAt(writeInPut.Count);
                }
            }
        }
        sw.Close();
    }

    private void SavingDataOnCsv()
    {
        GameContN gcRef = FindObjectOfType<GameContN>();
        List<string[]> stuffToSave = new List<string[]>();
        //save general datas
        string[] saveFileStringArray = { gcRef.playerDatas.lastCityVisited.ToString(), gcRef.playerDatas.lastNewsVisited, gcRef.playerDatas.lastSceneVisited.ToString() };
        stuffToSave.Add(saveFileStringArray);
        for (int i = 0; i < stuffToSave.Count; i++)
        {
            for (int j = 0; j < stuffToSave[i].Length; j++)
            {
              //  Debug.LogError(stuffToSave[i][j]);
            }
        }

        WriteCsv(path + "saveFile1.csv", stuffToSave);

        //load and create cities
        foreach (var cityToSave in gcRef.playerDatas.mapData)
        {
            string[] cityString = { cityToSave.mapName.ToString() };
            stuffToSave.Add(cityString);
            WriteCsv(path + "maps.csv", stuffToSave);
            foreach (var newsToSave in cityToSave.newsData)
            {
                stuffToSave.Clear();
                string[] newsString = { newsToSave.newsName, newsToSave.newsInfoText, newsToSave.playerCurrentScore.ToString() };
                stuffToSave.Add(newsString);
                WriteCsv(path + cityToSave.mapName + ".csv", stuffToSave);
                foreach (var sceneToSave in newsToSave.scenesData)
                {
                    stuffToSave.Clear();
                    string[] sceneString = { sceneToSave.sceneIndex.ToString() };
                    stuffToSave.Add(sceneString);
                    WriteCsv(path + newsToSave.newsName + ".csv", stuffToSave);
                    foreach (var clueToSave in sceneToSave.cluesData)
                    {
                        stuffToSave.Clear();
                        string[] clueString = { clueToSave.clueName, clueToSave.clueInfoText, clueToSave.hasBeenFound.ToString() };
                        stuffToSave.Add(clueString);
                        WriteCsv(path + newsToSave.newsName + sceneToSave.sceneIndex + ".csv", stuffToSave);
                    }
                }
            }
        }
        GameContN.Debugging("Data Saved");
    }
}