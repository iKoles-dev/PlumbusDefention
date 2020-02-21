using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Additional;
using TMPro;
using UnityEngine;

public class Player : ModifiedSingleton<Player>
{
    public GameObject TowerSpotPrefab;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private int _playerHealth = 100;
    public int PlayerMoney { get; private set; } = 100;

    public void ApplyDamage(int damage)
    {
        if (_playerHealth - damage <= 0)
        {
            _playerHealth = 0;
        }
        else
        {
            _playerHealth -= damage;
        }
        _healthText.text = _playerHealth.ToString();
    }

    public void ChangeMoney(int amount)
    {
        PlayerMoney += amount;
        _moneyText.text = PlayerMoney.ToString();
    }
}
