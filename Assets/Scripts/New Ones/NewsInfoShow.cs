using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class NewsInfoShow : MonoBehaviour
{

    public UnityEvent newsInfoShowRequest;

    void OnMouseEnter()
    {
        newsInfoShowRequest.Invoke();
    }
}
