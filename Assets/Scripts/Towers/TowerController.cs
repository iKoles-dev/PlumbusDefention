using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TowerController : MonoBehaviour
    {
        private BasicTower _towerControl;
        private Tower _tower;
        private int _level = 0;
        private List<EnemyController> _enemiesInRange = new List<EnemyController>();
        private Transform _currentPosition;

        private void Awake()
        {
            _towerControl = GetComponent<BasicTower>();
            _currentPosition = transform;
        }
        public void AddTowerPreferences(Tower tower)
        {
            _tower = tower;
            GetComponent<CircleCollider2D>().radius = _tower.Upgrades[_level].Range;
            StartCoroutine(TrackAndShoot());
        }
        public void SellTower()
        {
            Instantiate(Player.Instance.TowerSpotPrefab, transform.position, Quaternion.identity);
            Player.Instance.AddMoney(_tower.Upgrades[_level].SellCost);
            Destroy(gameObject);
        }

        private IEnumerator TrackAndShoot()
        {
            while (true)
            {
                ClearEnemyListFromNull();
                EnemyController nearestEnemy = GetNearestEnemy();
                if (nearestEnemy != null)
                {
                    _towerControl.Shoot(nearestEnemy,_tower.Upgrades[_level].Damage, _tower.Upgrades[_level].DamageRadius);
                }
                yield return new WaitForSeconds(_tower.Upgrades[_level].ShootInterval);
            }
        }
        private void ClearEnemyListFromNull()
        {
            for (int i = 0; i < _enemiesInRange.Count; i++)
            {
                if (_enemiesInRange[i] == null)
                {
                    _enemiesInRange.RemoveAt(i);
                    i--;
                    continue;
                }

            }
        }
        private EnemyController GetNearestEnemy()
        {
            if (_enemiesInRange.Count > 0)
            {
                EnemyController nearestEnemy = _enemiesInRange[0];
                float nearestDistance = Vector3.Distance(_currentPosition.position, nearestEnemy.transform.position);
                _enemiesInRange.ForEach(enemy =>
                {
                    float newDistance = Vector3.Distance(_currentPosition.position, enemy.transform.position);
                    if (newDistance < nearestDistance)
                    {
                        nearestEnemy = enemy;
                        nearestDistance = newDistance;
                    }
                });
                return nearestEnemy;
            }
            else
            {
                return null;
            }

        }
        
        private void OnTriggerEnter2D(Collider2D enteredCollider)
        {
            if (enteredCollider.GetComponent<EnemyController>()!=null)
            {
                _enemiesInRange.Add(enteredCollider.GetComponent<EnemyController>());
            }
        }
        private void OnTriggerExit2D(Collider2D leftCollider)
        {
            if (leftCollider.GetComponent<EnemyController>() != null)
            {
                _enemiesInRange.Remove(leftCollider.GetComponent<EnemyController>());
            }
        }

    }
}