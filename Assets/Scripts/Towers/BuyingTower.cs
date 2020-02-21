using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Towers;
using TMPro;
using UnityEngine;

public class BuyingTower : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private TextMeshProUGUI _price;
    private TowerSpot _towerSpot;

    private void Start()
    {
        _price.text = _tower.BuyingCost.ToString();
        _towerSpot = GetComponentInParent<TowerSpot>();
    }

    public void BuyTower()
    {
        if (Player.Instance.PlayerMoney >= _tower.BuyingCost)
        {
            GameObject newTower = Instantiate(_tower.TowerPrefab, _towerSpot.transform.position, Quaternion.identity);
            newTower.GetComponent<TowerController>().AddTowerPreferences(_tower);
            Destroy(_towerSpot.gameObject);
        }
    }
}
