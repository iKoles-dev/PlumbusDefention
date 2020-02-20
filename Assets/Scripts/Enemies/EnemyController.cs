using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyStates _state;
        private EnemyСharacteristics _enemyСharacteristics;
        private List<Vector3> _path = new List<Vector3>();
        private int _currentPathPoint = 0;
        public void SetСharacteristics(EnemyСharacteristics enemyСharacteristics, List<Vector3> pathPoints)
        {
            _path = pathPoints;
            _enemyСharacteristics = enemyСharacteristics;
            transform.position = _path[0];
            _state = EnemyStates.Walk;
            StartCoroutine(Walk());
        }

        private IEnumerator Walk()
        {
            while (true)
            {
                Vector3.MoveTowards(transform.position, _path[_currentPathPoint], _enemyСharacteristics.MoveSpeed);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}