using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private List<Wave> _allWaves = new List<Wave>();

    private void Start()
    {
        StartCoroutine(_spawner.Spawn(_allWaves[0].EnemiesInWave[0]));
    }
}
