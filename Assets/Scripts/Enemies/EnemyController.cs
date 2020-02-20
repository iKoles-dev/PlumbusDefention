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
        public void SetСharacteristics(Enemy enemy, List<Vector3> pathPoints)
        {
            _path = pathPoints;
            _enemy = enemy;
            transform.position = _path[0];
            _state = EnemyStates.Walk;
            StartCoroutine(Walk());
        }

        private IEnumerator Walk()
        {
            while (true)
            {
                Vector3.MoveTowards(transform.position, _path[_currentPathPoint], _enemy.MoveSpeed);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}