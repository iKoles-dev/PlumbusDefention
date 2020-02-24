using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedUp : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public void OnPointerUp(PointerEventData eventData)
    {
        Time.timeScale = 1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 4;
    }
}
