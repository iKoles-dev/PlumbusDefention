using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Additional;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : ModifiedSingleton<Player>
{
    public GameObject TowerSpotPrefab;
    [SerializeField] private GameObject _failGame;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private int _playerHealth = 100;
    [SerializeField] private CastleDestructor _castleDestructor;

    private void Start()
    {
        _moneyText.text = PlayerMoney.ToString();
        _healthText.text = _playerHealth.ToString();
    }

    public int PlayerMoney;

    public void ApplyDamage(int damage)
    {
        if (_playerHealth - damage <= 0)
        {
            _playerHealth = 0;
            _failGame.SetActive(true);
        }
        else
        {
            _playerHealth -= damage;
        }
        _healthText.text = _playerHealth.ToString();
        if (_playerHealth == 0)
        {
            _castleDestructor.SetDestroyedSprite();
        }
        else if(_playerHealth<50)
        {
            _castleDestructor.SetDamagedSprite();
        }
    }

    public void ChangeMoney(int amount)
    {
        PlayerMoney += amount;
        _moneyText.text = PlayerMoney.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
