using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClueCustomClickEvent : MonoBehaviour, IPointerClickHandler
{
	public string clueName;

    public event_string customClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        customClick.Invoke(clueName);
    }
}
