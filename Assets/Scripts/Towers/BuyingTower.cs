using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyingTower : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private TextMeshProUGUI _price;
    private Button _buyButton;
    private TowerSpot _towerSpot;

    private void Start()
    {
        _price.text = _tower.BuyingCost.ToString();
        _towerSpot = GetComponentInParent<TowerSpot>();
        _buyButton = GetComponentInChildren<Button>();
    }
    public void BuyTower()
    {
        if (Player.Instance.PlayerMoney >= _tower.BuyingCost)
        {
            Player.Instance.ChangeMoney(-_tower.BuyingCost);
            GameObject newTower = Instantiate(_tower.TowerPrefab, _towerSpot.transform.position, Quaternion.identity);
            newTower.GetComponent<TowerController>().AddTowerPreferences(_tower);
            newTower.GetComponent<SpriteRenderer>().sortingLayerName = transform.parent.gameObject.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingLayerName;
            Destroy(_towerSpot.gameObject);
        }
    }
}
