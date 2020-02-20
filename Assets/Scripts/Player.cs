using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Additional;
using TMPro;
using UnityEngine;

public class Player : ModifiedSingleton<Player>
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private int _playerHealth = 100;
    [SerializeField] private int _playerMoney = 100;

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
}
