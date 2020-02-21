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
        if (_allWaves[_currentWave].EnemiesInWave.Count > _enemiesInWave + 2)
        {
            _enemiesInWave++;
            StartCoroutine(_spawner.Spawn(_allWaves[_currentWave].EnemiesInWave[_enemiesInWave]));
        }
    }

    private IEnumerator CountDown()
    {
        for (int i = 0; i < _allWaves.Count; i++)
        {
            _waves.text = $"{_currentWave + 1}/{_allWaves.Count}";
            //Время для установки башен
            if (i == 0)
            {
                for (int j = 0; j <= _timeToFirstSpawn; _timeToFirstSpawn--)
                {
                    _nextWave.text = "Next wave: " + _timeToFirstSpawn+"s";
                    yield return new WaitForSeconds(1);
                }
            }

            StartCoroutine(_spawner.Spawn(_allWaves[_currentWave].EnemiesInWave[_enemiesInWave]));
            _timeToNextWave = (int) _allWaves[_currentWave].TimeToNextWave;
            for (int j = 0; j <= _timeToNextWave; _timeToNextWave--)
            {
                _nextWave.text = "Next wave: " + _timeToNextWave+"s";
                yield return new WaitForSeconds(1);
            }
        }
    }
}
