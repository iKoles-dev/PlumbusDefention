using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyingTower : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private TextMeshProUGUI _price;

    private void Start()
    {
        _price.text = _tower.BuyingCost.ToString();
    }
}
