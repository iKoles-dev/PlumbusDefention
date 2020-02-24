using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private int _timeToFirstSpawn;
    [SerializeField] private TextMeshProUGUI _waves;
    [SerializeField] private TextMeshProUGUI _nextWave;
    [SerializeField] private List<Wave> _allWaves = new List<Wave>();
    [SerializeField] private GameObject _winGame;
    private Spawner _spawner;
    private int _enemiesInWave = 0;
    private int _currentWave = 0;
    private int _timeToNextWave = 0;

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        _spawner.OnSpawnEnd += SpawnNextEnemiesInWave;
        StartCoroutine(CountDown());
    }

    private void SpawnNextEnemiesInWave()
    {
        StartCoroutine(IenumSpawnNextEnemiesInWave());
    }

    private IEnumerator IenumSpawnNextEnemiesInWave()
    {
        yield return new WaitForSeconds(_allWaves[_currentWave].EnemiesInWave[_enemiesInWave].TimeToNextEnemies);
        if (_allWaves[_currentWave].EnemiesInWave.Count > _enemiesInWave + 1)
        {
            _enemiesInWave++;
            StartCoroutine(_spawner.Spawn(_allWaves[_currentWave].EnemiesInWave[_enemiesInWave]));
        }
        else
        {
            _currentWave++;
            _enemiesInWave = 0;
        }

        if (_allWaves.Count == _currentWave + 1 && _enemiesInWave+1 == _allWaves[_currentWave].EnemiesInWave.Count)
        {
            StartCoroutine(WaitingForGameWin());
        }
    }

    private IEnumerator CountDown()
    {

        //Time given to Player for setting tower
        _waves.text = $"Wave {_currentWave + 1}/{_allWaves.Count}";
        for (int j = 0; j <= _timeToFirstSpawn; _timeToFirstSpawn--)
        {
            _nextWave.text = "Next wave: " + _timeToFirstSpawn + "s";
            yield return new WaitForSeconds(1);
        }
        foreach (var wave in _allWaves)
        {
            _waves.text = $"Wave {_currentWave + 1}/{_allWaves.Count}";
            StartCoroutine(_spawner.Spawn(_allWaves[_currentWave].EnemiesInWave[_enemiesInWave]));
            _timeToNextWave = (int) _allWaves[_currentWave].TimeToNextWave;
            for (int j = 0; j <= _timeToNextWave; _timeToNextWave--)
            {
                _nextWave.text = "Next wave: " + _timeToNextWave+"s";
                yield return new WaitForSeconds(1);
            }
        }
    }

    private IEnumerator WaitingForGameWin()
    {
        yield return new WaitForSeconds(_allWaves[_currentWave].EnemiesInWave[_enemiesInWave].EnemiesAmount *
            _allWaves[_currentWave].EnemiesInWave[_enemiesInWave].TimeBetweenSpawns + 5);
        bool hasEnemy = true;
        while (hasEnemy)
        {
            hasEnemy = false;
            Collider2D[] allEnimiesInRange = Physics2D.OverlapCircleAll(transform.position, 200);
            foreach (var enemyCollider in allEnimiesInRange)
            {
                if (enemyCollider.gameObject.GetComponent<EnemyController>() != null)
                {
                    hasEnemy = true;
                }
            }

            yield return new WaitForSeconds(1);
        }
        _winGame.SetActive(true);
    }
}
