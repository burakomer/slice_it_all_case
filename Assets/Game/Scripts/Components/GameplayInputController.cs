using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayInputController : MonoBehaviour, IPointerDownHandler
{
    public event Action Tapped;


    public void OnPointerDown(PointerEventData eventData)
    {
        Tapped?.Invoke();
    }
}