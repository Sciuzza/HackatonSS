using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InputController : MonoBehaviour {

    #region Events
    public UnityEvent escapeRequest; 
    #endregion

    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
            escapeRequest.Invoke();

        

    }
}
