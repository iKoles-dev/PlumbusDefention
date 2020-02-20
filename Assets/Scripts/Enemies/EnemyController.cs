using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyStates _state;
        private Enemy _enemy;
        private List<Vector3> _path = new List<Vector3>();
        private int _currentPathPoint = 0;
        private Transform _transform;
        private Vector3 _halfHeight;
        private Vector3 GetNextPoint => _path[_currentPathPoint] + _halfHeight;
        private Animator _animator;
        private float _health;
        private SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void SetСharacteristics(Enemy enemy, List<Vector3> pathPoints)
        {
            _halfHeight = new Vector3(0, enemy.EnemyImage.bounds.size.y / 2 * transform.localScale.y, 0);
            _path = pathPoints;
            _enemy = enemy;
            transform.position = GetNextPoint;
            _currentPathPoint++;
            _state = EnemyStates.Walk;
            _health = _enemy.Health;
            StartCoroutine(Walk());
        }

        private IEnumerator Walk()
        {
            ChangeSpriteFlip();
            SetAnimation();
            while (_state == EnemyStates.Walk)
            {
                _transform.position = Vector3.MoveTowards(transform.position, GetNextPoint, _enemy.MoveSpeed/500);
                if (_transform.position == GetNextPoint)
                {
                    if (_path.Count <= _currentPathPoint+1)
                    {
                        _state = EnemyStates.Idle;
                        StartCoroutine(Attack());
                    }
                    else
                    {
                        _currentPathPoint++;
                        ChangeSpriteFlip();
                    }
                }
                yield return new WaitForEndOfFrame();
            }
        }
        private IEnumerator Attack()
        {
            while (_state == EnemyStates.Idle || _state == EnemyStates.Attack)
            {
                SetAnimation();
                yield return new WaitForSeconds(_enemy.AttackSpeed);
                if (_state == EnemyStates.Die)
                {
                    break;
                }

                _state = EnemyStates.Attack;
                SetAnimation();
                yield return new WaitForSeconds(1); //Attack animation duration
                if (_state == EnemyStates.Die)
                {
                    break;
                }
                Player.Instance.ApplyDamage(_enemy.AttackStrength);
                _state = EnemyStates.Idle;
            }
        }
        public void ApplyDamage(float damage)
        {
            if (_health - damage <= 0)
            {
                _health = 0;
                _state = EnemyStates.Die;
                StartCoroutine(Die());
            }
            else
            {
                _health -= damage;
                if (_state == EnemyStates.Walk)
                {
                    StartCoroutine(Damage());
                }
            }
        }
        private IEnumerator Damage()
        {
            SetAnimation();
            yield return new WaitForSeconds(1);
            if (_state != EnemyStates.Die)
            {
                _state = EnemyStates.Walk;
                StartCoroutine(Walk());
            }
        }
        private IEnumerator Die()
        {
            SetAnimation();
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
        private void SetAnimation()
        {
            _animator.SetTrigger(_state.ToString());
        }
        private void ChangeSpriteFlip()
        {
            bool flipSide = _path[_currentPathPoint-1].x - _path[_currentPathPoint].x > 0;
            _spriteRenderer.flipX = flipSide;
        }
    }
}