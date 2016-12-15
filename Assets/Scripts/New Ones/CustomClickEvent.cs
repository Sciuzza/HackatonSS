using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CustomClickEvent : MonoBehaviour, IPointerClickHandler
{

    public int buttonIndex;
    public int soundIndex;
    public event_int customClick;
    public event_int customClickSound;
    public bool customInteractable = true;

    void Awake()
    {
        soundIndex = UnityEngine.Random.Range(0, 19);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (customInteractable)
        {
            customClick.Invoke(buttonIndex);
            customClickSound.Invoke(soundIndex);

        }
    }

 
}
