using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CustomClickEvent : MonoBehaviour, IPointerClickHandler
{

    public int buttonIndex;

    public event_int customClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        customClick.Invoke(buttonIndex);
    }

 
}
