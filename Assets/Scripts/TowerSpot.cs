using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _menu;
    private bool _isMenuShown = false;
    public void MenuTapped()
    {
        _isMenuShown = !_isMenuShown;
        _menu.SetActive(_isMenuShown);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MenuTapped();
    }
}
