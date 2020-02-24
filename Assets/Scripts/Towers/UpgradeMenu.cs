using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentUpgradeLevel;
    [SerializeField] private TextMeshProUGUI _upgradeCost;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private TextMeshProUGUI _sellCost;
    private TowerController _towerController;
    private void Start()
    {
        _towerController = GetComponentInParent<TowerController>();
        RefreshParametres();
    }

    public void Upgrade()
    {
        Player.Instance.ChangeMoney(-_towerController.CurrentTower.Upgrades[_towerController.Level+1].Cost);
        _towerController.Upgrade();
        RefreshParametres();
    }

    public void Sell()
    {
        Player.Instance.ChangeMoney(_towerController.CurrentTower.Upgrades[_towerController.Level].SellCost);
        Instantiate(Player.Instance.TowerSpotPrefab, _towerController.gameObject.transform.position, Quaternion.identity);
        Destroy(_towerController.gameObject);
    }

    private void Update()
    {
        if (_towerController.CurrentTower.Upgrades.Count > _towerController.Level + 1)
        {
            _upgradeButton.interactable = Player.Instance.PlayerMoney >= _towerController.CurrentTower.Upgrades[_towerController.Level + 1].Cost;
        }
        else
        {
            _upgradeButton.interactable = false;
        }
    }

    private void RefreshParametres()
    {
        _currentUpgradeLevel.text = "Level "+(_towerController.Level+1);
        if (_towerController.CurrentTower.Upgrades.Count > _towerController.Level + 1)
        {
            _upgradeCost.text = _towerController.CurrentTower.Upgrades[_towerController.Level + 1].Cost.ToString();
        }
        else
        {
            _upgradeCost.text = "Upgraded";
        }

        _sellCost.text = "Sell: " + _towerController.CurrentTower.Upgrades[_towerController.Level].SellCost;
    }
}
