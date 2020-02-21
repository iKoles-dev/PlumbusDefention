using Assets.Scripts.Ammunition;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class ControlBallTower : BasicTower
    {
        [SerializeField] private GameObject _fireBall;
        public override void Shoot(EnemyController enemy, float damage, float radius)
        {
            GameObject fireBall = Instantiate(_fireBall, transform.position, Quaternion.identity);
            fireBall.GetComponent<ControlBall>().SetTargetPoint(enemy.transform, damage, radius);
        }
    }
}