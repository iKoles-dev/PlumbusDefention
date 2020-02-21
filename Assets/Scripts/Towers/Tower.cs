using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Towers;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Create Tower", order = 53)]
public class Tower : ScriptableObject
{
    public Sprite FirstTowerUpgrade;
    public Sprite SecondTowerUpgrade;
    public int LevelToFirstUpgrade;
    public Sprite ThirdTowerUpgrade;
    public int LevelToSecondUpgrade;
    public List<TowerUpgrades> Upgrades = new List<TowerUpgrades>();
    public Animator ShootAnimation = null;
    public BasicTower TowerBehaviour;
    public int BuyingCost;

}
