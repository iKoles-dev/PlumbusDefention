using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Towers
{
    public class TowerController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _upgradeMenu;
        [SerializeField] private Transform _range;
        private Vector3 _rangeScale = new Vector3(0.4f, 0.4f, 0.4f);
        private BasicTower _towerControl;
        [HideInInspector] public Tower CurrentTower;
        public int Level;
        private Transform _currentPosition;

        private void Awake()
        {
            _towerControl = GetComponent<BasicTower>();
            _currentPosition = transform;
        }
        public void AddTowerPreferences(Tower tower)
        {
            CurrentTower = tower;
            _range.localScale = _rangeScale * CurrentTower.Upgrades[Level].Range;
            StartCoroutine(TrackAndShoot());
        }
        public void SellTower()
        {
            Instantiate(Player.Instance.TowerSpotPrefab, transform.position, Quaternion.identity);
            Player.Instance.ChangeMoney(CurrentTower.Upgrades[Level].SellCost);
            Destroy(gameObject);
        }

        private IEnumerator TrackAndShoot()
        {
            while (true)
            {
                EnemyController nearestEnemy = GetNearestEnemy();
                if (nearestEnemy != null)
                {
                    _towerControl.Shoot(nearestEnemy,CurrentTower.Upgrades[Level].Damage, CurrentTower.Upgrades[Level].DamageRadius);
                    yield return new WaitForSeconds(CurrentTower.Upgrades[Level].ShootInterval);
                }
                else
                {
                    yield return new WaitForFixedUpdate();
                }
            }
        }
        private EnemyController GetNearestEnemy()
        {
            Collider2D[] allCollidersInRange = Physics2D.OverlapCircleAll(_currentPosition.position, CurrentTower.Upgrades[Level].Range/2);
            List<EnemyController> allEnemiesInRange = new List<EnemyController>();
            foreach (var collider2D in allCollidersInRange)
            {
                if (collider2D.TryGetComponent<EnemyController>(out EnemyController enemy))
                {
                    allEnemiesInRange.Add(enemy);
                }
            }
            if (allEnemiesInRange.Count > 0)
            {
                EnemyController nearestEnemy = allEnemiesInRange[0];
                float nearestDistance = Vector3.Distance(_currentPosition.position, nearestEnemy.transform.position);
                allEnemiesInRange.ForEach(enemy =>
                {

                    if (enemy.State != EnemyStates.Die)
                    {
                        float newDistance = Vector3.Distance(_currentPosition.position, enemy.transform.position);
                        if (newDistance < nearestDistance)
                        {
                            nearestEnemy = enemy;
                            nearestDistance = newDistance;
                        }
                    }
                });
                if (nearestEnemy.State == EnemyStates.Die)
                {
                    return null;
                }
                return nearestEnemy;
            }
            else
            {
                return null;
            }

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _upgradeMenu.SetActive(true);
        }

        public void Upgrade()
        {
            Level++;
            if (CurrentTower.LevelToFirstUpgrade <= Level + 1)
            {
                GetComponent<SpriteRenderer>().sprite = CurrentTower.SecondTowerUpgrade;
            }
            else if (CurrentTower.LevelToSecondUpgrade <= Level + 1)
            {
                GetComponent<SpriteRenderer>().sprite = CurrentTower.ThirdTowerUpgrade;
            }
            _range.localScale = _rangeScale * CurrentTower.Upgrades[Level].Range;
        }
        
    }
}