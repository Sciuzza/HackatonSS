using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{

	public string path = Application.persistentDataPath;

	#region Taking References and Linking Events
	void Awake()
	{
		GameContN gcTempLink = this.gameObject.GetComponent<GameContN>();

		gcTempLink.loadDataRequest.AddListener(LoadingDataFromCsv);
		gcTempLink.saveDataRequest.AddListener(SavingDataOnCsv);

	}
	#endregion

	private void LoadingDataFromCsv()
	{
		string[] tempPaths = new string[];
		ReadCsv(path + "maps.csv");

        // fede qui ci sei tu, i dati che la logica utilizzerà sono nella variabile GameContN.playerdataStati, salva tutto li dentro

        GameContN.Debugging("Data Loaded");

      
    }

	private void ReadCsv(string _fileName, out List<string> readOutPut) 
	{
		List<string> readOutPut = new List<string>();
		int counter = 0;
		StreamReader sr = File.OpenText(path + _fileName);
		string s;
		while ((s = sr.ReadLine()) != null)
		{
			if (counter == 0)
			{
				counter++;
				continue;
			}
			readOutPut.Add(sr.ReadLine());
	} 

    private void SavingDataOnCsv()
    {

        // fede qui ci sei tu, i dati che la logica utilizzerà sono nella variabile GameContN.playerdataStatic, carica tutto da li
        GameContN.Debugging("Data Saved");
    }
}
