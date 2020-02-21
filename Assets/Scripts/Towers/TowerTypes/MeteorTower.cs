using Assets.Scripts.Ammunition;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class MeteorTower : BasicTower
    {
        [SerializeField] private GameObject _meteor;
        public override void Shoot(EnemyController enemy, float damage, float radius)
        {
            GameObject fireBall = Instantiate(_meteor, new Vector3(0,12,0), Quaternion.identity);
            fireBall.GetComponent<ControlBall>().SetTargetPoint(enemy.transform, damage, radius);
        }
    }
}