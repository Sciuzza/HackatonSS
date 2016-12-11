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

        ReadCsv(path + "saveFile.csv", out nextStepPath);
        //load saveFile
        GameContN.Self.playerDatas.mapData = new List<sensibleMapData>();
        GameContN.Self.playerDatas.lastCityVisited = nextStepPath[0][0];
        GameContN.Self.playerDatas.lastNewsVisited = nextStepPath[0][1];
        GameContN.Self.playerDatas.lastSceneVisited = int.Parse(nextStepPath[0][2]);
        
        //load and create cities
        ReadCsv(path + "maps.csv", out nextStepPath);

        for (int i = 0; i < nextStepPath.Count; i++)
        {
            sensibleMapData newCity = new sensibleMapData();

            newCity.mapName = nextStepPath[0][i];
            newCity.newsData = new List<sensibleNewsData>();
            GameContN.Self.playerDatas.mapData.Add(newCity);

            //load and create news
            ReadCsv(path + nextStepPath[0][i] + ".csv", out nextStepPath);
            for (int j = 0; j < nextStepPath.Count; j++)
            {
                sensibleNewsData newNews = new sensibleNewsData();
                newNews.scenesData = new List<sensibleSceneData>();

                newNews.newsName = nextStepPath[0][j];
                newNews.newsInfoText = nextStepPath[1][j];
                newNews.playerCurrentScore = int.Parse(nextStepPath[2][j]);
                newCity.newsData.Add(newNews);

                //load and create scenes
                ReadCsv(path + nextStepPath[0][j] + ".csv", out nextStepPath);
                for (int k = 0; k < nextStepPath.Count; k++)
                {
                    sensibleSceneData newScene = new sensibleSceneData();
                    newScene.cluesData = new List<sensibleClueData>();

                    newScene.sceneIndex = int.Parse(nextStepPath[0][k]);
                    newNews.scenesData.Add(newScene);
                    //load and create clues
                    ReadCsv(path + nextStepPath[0][k] + ".csv", out nextStepPath);
                    for (int l = 0; l < nextStepPath.Count; l++)
                    {
                        sensibleClueData newClue = new sensibleClueData();

                        newClue.clueName = nextStepPath[0][l];
                        newClue.clueInfoText = nextStepPath[1][l];
                        newClue.hasBeenFound = bool.Parse(nextStepPath[2][j]);
                        newScene.cluesData.Add(newClue);
                    }
                }
            }
        }
        GameContN.Debugging("Data Loaded");
    }

    private void ReadCsv(string _fileName, out List<string[]> readOutPut)
    {
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
            Debug.Log(s);
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