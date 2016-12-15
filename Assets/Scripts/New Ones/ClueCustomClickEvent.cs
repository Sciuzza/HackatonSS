using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClueCustomClickEvent : MonoBehaviour, IPointerClickHandler
{
    public string clueInfoText = null;
    public int soundIndex;
    public event_string customClick = new event_string();

    public bool customInteractable = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (customInteractable)
            customClick.Invoke(clueInfoText);
    }
}
