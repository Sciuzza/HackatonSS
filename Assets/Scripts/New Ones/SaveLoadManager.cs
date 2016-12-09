using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour {


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

        // fede qui ci sei tu, i dati che la logica utilizzerà sono nella variabile GameContN.playerdataStati, salva tutto li dentro

        GameContN.Debugging("Data Loaded");

      
    }

    private void SavingDataOnCsv()
    {

        // fede qui ci sei tu, i dati che la logica utilizzerà sono nella variabile GameContN.playerdataStatic, carica tutto da li
        GameContN.Debugging("Data Saved");
    }
}
