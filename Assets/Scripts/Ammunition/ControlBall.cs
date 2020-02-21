using System.Collections;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Ammunition
{
    public class ControlBall : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Transform _transform;
        private float _radius;
        private float _damage;
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void SetTargetPoint(Transform target, float damage, float radius)
        {
            _damage = damage;
            _radius = radius;
            StartCoroutine(Move(target));
        }

        private IEnumerator Move(Transform target)
        {
            while (target!=null && Vector3.Distance(_transform.position,target.position) > _radius/2)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, target.position, _speed / 100);
                yield return new WaitForEndOfFrame();
            }

            Collider2D[] allEnimiesInRange = Physics2D.OverlapCircleAll(_transform.position, _radius);
            foreach (var enemy in allEnimiesInRange)
            {
                if (enemy.GetComponent<EnemyController>() != null && enemy.GetComponent<EnemyController>().State != EnemyStates.Die)
                {
                    enemy.GetComponent<EnemyController>().ApplyDamage((int)_damage);
                }
            }
            Destroy(gameObject);
        }
    }
}