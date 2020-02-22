using System.Collections;
using System.Collections.Generic;
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
        private List<EnemyController> _enemiesInRange = new List<EnemyController>();
        private Transform _currentPosition;

        private void Awake()
        {
            _towerControl = GetComponent<BasicTower>();
            _currentPosition = transform;
        }
        public void AddTowerPreferences(Tower tower)
        {
            CurrentTower = tower;
            GetComponent<CircleCollider2D>().radius = CurrentTower.Upgrades[Level].Range;
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
                ClearEnemyListFromNull();
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