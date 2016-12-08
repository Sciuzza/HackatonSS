using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class NewsInfoShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Public Variables
    public string newsInfoText;
    #endregion

    #region Events
    public event_string newsInfoShowRequest;
    public UnityEvent newsInfoDisableRequest;
    #endregion

    #region Mouse Users
    public void OnPointerExit(PointerEventData eventData)
    {
        newsInfoDisableRequest.Invoke();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        newsInfoShowRequest.Invoke(newsInfoText);
    } 
    #endregion
}
