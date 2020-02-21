using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyStates State;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private GameObject _healthPanel;
        private Enemy _enemy;
        private List<Vector3> _path = new List<Vector3>();
        private int _currentPathPoint = 0;
        private Transform _transform;
        private Vector3 _halfHeight;
        private Vector3 GetNextPoint => _path[_currentPathPoint] + _halfHeight;
        private Animator _animator;
        private int _health;
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
            State = EnemyStates.Walk;
            _health = _enemy.Health;

            _healthPanel.transform.localPosition = _halfHeight*2;
            _healthText.text = _enemy.Health.ToString();

            StartCoroutine(Walk());
        }

        private IEnumerator Walk()
        {
            ChangeSpriteFlip();
            SetAnimation();
            while (State == EnemyStates.Walk)
            {
                _transform.position = Vector3.MoveTowards(transform.position, GetNextPoint, _enemy.MoveSpeed/500);
                if (_transform.position == GetNextPoint)
                {
                    if (_path.Count <= _currentPathPoint+1)
                    {
                        State = EnemyStates.Idle;
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
            while (State == EnemyStates.Idle || State == EnemyStates.Attack)
            {
                SetAnimation();
                yield return new WaitForSeconds(_enemy.AttackSpeed);
                if (State == EnemyStates.Die)
                {
                    break;
                }

                State = EnemyStates.Attack;
                SetAnimation();
                yield return new WaitForSeconds(1); //Attack animation duration
                if (State == EnemyStates.Die)
                {
                    break;
                }
                Player.Instance.ApplyDamage(_enemy.AttackStrength);
                State = EnemyStates.Idle;
            }
        }
        public void ApplyDamage(int damage)
        {
            if (_health - damage <= 0)
            {
                _health = 0;
                State = EnemyStates.Die;
                StartCoroutine(Die());
            }
            else
            {
                _health -= damage;
                if (State == EnemyStates.Walk)
                {
                    StartCoroutine(Damage());
                }
            }
            _healthText.text = ((int)_health).ToString();
        }
        private IEnumerator Damage()
        {
            SetAnimation();
            yield return new WaitForSeconds(1);
            if (State != EnemyStates.Die)
            {
                State = EnemyStates.Walk;
                StartCoroutine(Walk());
            }
        }
        private IEnumerator Die()
        {
            SetAnimation();
            Player.Instance.ChangeMoney(_enemy.Health/3);
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
        private void SetAnimation()
        {
            _animator.SetTrigger(State.ToString());
        }
        private void ChangeSpriteFlip()
        {
            bool flipSide = _path[_currentPathPoint-1].x - _path[_currentPathPoint].x > 0;
            _spriteRenderer.flipX = flipSide;
        }
    }
}