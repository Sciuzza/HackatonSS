using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{

    public string path;

    #region Taking References and Linking Events
    void Awake()
    {
        path = Application.persistentDataPath + "/";
        GameContN gcTempLink = this.gameObject.GetComponent<GameContN>();

        gcTempLink.loadDataRequest.AddListener(LoadingDataFromCsv);
        gcTempLink.saveDataRequest.AddListener(SavingDataOnCsv);

    }
    #endregion

    private void LoadingDataFromCsv()
    {
        List<string[]> nextStepPath = new List<string[]>();
        List<string[]> nextStepPath1 = new List<string[]>();
        List<string[]> nextStepPath5 = new List<string[]>();
        List<string[]> nextStepPath6 = new List<string[]>();
        List<string[]> nextStepPath8 = new List<string[]>();
        List<List<string[]>> nextStepPath2 = new List<List<string[]>>();
        List<List<string[]>> nextStepPath7 = new List<List<string[]>>();
        List<List<string[]>> nextStepPath9 = new List<List<string[]>>();
        List<List<List<string[]>>> nextStepPath3 = new List<List<List<string[]>>>();
        List<List<List<string[]>>> nextStepPath10 = new List<List<List<string[]>>>();
        List<List<List<List<string[]>>>> nextStepPath4 = new List<List<List<List<string[]>>>>();

        ReadCsv(path + "saveFile.csv", out nextStepPath);
        //load saveFile
        GameContN.Self.playerDatas.mapData = new List<sensibleMapData>();
        GameContN.Self.playerDatas.lastCityVisited = nextStepPath[0][0];
        GameContN.Self.playerDatas.lastNewsVisited = nextStepPath[0][1];
        GameContN.Self.playerDatas.lastSceneVisited = int.Parse(nextStepPath[0][2]);

        //load and create cities
        ReadCsv(path + "maps.csv", out nextStepPath1);

        sensibleMapData newCity;
        for (int i = 0; i < nextStepPath1.Count; i++)
        {
            newCity = new sensibleMapData();

            newCity.mapName = nextStepPath1[i][0];
            newCity.newsData = new List<sensibleNewsData>();
            GameContN.Self.playerDatas.mapData.Add(newCity);

            //load and create news
            ReadCsv(path + nextStepPath1[i][0] + ".csv", out nextStepPath5);
            nextStepPath2.Add(nextStepPath5);
        }
        sensibleNewsData newNews;
        for (int i = 0; i < nextStepPath2.Count; i++)
        {
            for (int j = 0; j < nextStepPath2[i].Count; j++)
            {    
                newNews = new sensibleNewsData();
                newNews.scenesData = new List<sensibleSceneData>();

                newNews.newsName = nextStepPath2[i][j][0];
                newNews.newsInfoText = nextStepPath2[i][j][1];
                newNews.playerCurrentScore = int.Parse(nextStepPath2[i][j][2]);

                GameContN.playerDatasStatic.mapData[i].newsData.Add(newNews);
                //load and create scenes

                ReadCsv(path + nextStepPath2[i][j][0] + ".csv", out nextStepPath6);
                nextStepPath7.Add(nextStepPath6);
                nextStepPath3.Add(nextStepPath7);
            }
        }
        sensibleSceneData newScene;
        for (int i = 0; i < nextStepPath3.Count; i++)
        {
            for (int j = 0; j < nextStepPath3[i].Count; j++)
            {
                for (int k = 0; k < nextStepPath3[i][j].Count; k++)
                {
                    newScene = new sensibleSceneData();
                    newScene.cluesData = new List<sensibleClueData>();

                    newScene.sceneIndex = int.Parse(nextStepPath3[i][j][k][0]);

                    GameContN.playerDatasStatic.mapData[i].newsData[j].scenesData.Add(newScene);
                    //load and create clues
   
                    int tempN = GameContN.playerDatasStatic.mapData[i].newsData[j].scenesData.Count;
                    if (k<tempN-1)
                    {
                        ReadCsv(path + GameContN.playerDatasStatic.mapData[i].newsData[j].newsName + nextStepPath3[i][j][k][0] + ".csv", out nextStepPath8);
                    }
                    
                    nextStepPath9.Add(nextStepPath8);
                    nextStepPath10.Add(nextStepPath9);
                    nextStepPath4.Add(nextStepPath10);
                }
            }
        }
        sensibleClueData newClue;
        for (int i = 0; i < nextStepPath4.Count; i++)
        {
            for (int j = 0; j < nextStepPath4[i].Count; j++)
            {
                for (int k = 0; k < nextStepPath4[i][j].Count; k++)
                {
                    for (int h = 0; h < nextStepPath4[i][j][k].Count; h++)
                    {
                        newClue = new sensibleClueData();

                        newClue.clueName = nextStepPath4[i][j][k][h][0];
                        newClue.clueInfoText = nextStepPath4[i][j][k][h][1];
                        newClue.hasBeenFound = bool.Parse(nextStepPath4[i][j][k][h][2]);

                        GameContN.playerDatasStatic.mapData[i].newsData[j].scenesData[k].cluesData.Add(newClue);
                    }
                }
            }
        }
        GameContN.Debugging("Data Loaded");
    }

    private void ReadCsv(string _fileName, out List<string[]> readOutPut)
    {
        Debug.Log(_fileName);
        readOutPut = new List<string[]>();
        char[] separator = { ',' };

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
        char[] separator = { ',' };
        writeInPut = new List<string[]>();
        int counter = 0;
        StreamWriter sw = File.AppendText(_fileName);
        string s = "";
        while (writeInPut.Count > 0)
        {
            if (counter == 0)
            {
                counter++;
                continue;
            }
            for (int i = 0; i < writeInPut.Count; i++)
            {
                for (int j = 0; j < writeInPut[i].Length; j++)
                {
                    if (j < writeInPut[i].Length - 1)
                    {
                        s += writeInPut[i][j] + ',';
                    }
                    else
                    {
                        s += writeInPut[i][j];
                    }

                }
                sw.WriteLine(s);
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
        WriteCsv(path + "saveFile.csv", stuffToSave);


        //load and create cities
        //WriteCsv(path + "maps.csv", nextStepPath);

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
                WriteCsv(path + cityToSave.mapName, stuffToSave);
                foreach (var sceneToSave in newsToSave.scenesData)
                {
                    stuffToSave.Clear();
                    string[] sceneString = { sceneToSave.sceneIndex.ToString() };
                    stuffToSave.Add(sceneString);
                    WriteCsv(path + newsToSave.newsName, stuffToSave);
                    foreach (var clueToSave in sceneToSave.cluesData)
                    {
                        stuffToSave.Clear();
                        string[] clueString = { clueToSave.clueName, clueToSave.clueInfoText, clueToSave.hasBeenFound.ToString() };
                        stuffToSave.Add(clueString);
                        WriteCsv(path + newsToSave.newsName + sceneToSave.sceneIndex, stuffToSave);
                    }
                }
            }
        }
        GameContN.Debugging("Data Loaded");
    }
}