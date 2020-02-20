﻿using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Scripts;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private PathCreator _pathCreator;
    [HideInInspector] public Event OnSpawnEnd;

    public IEnumerator Spawn(EnemiesProperties enemiesProperties)
    {
        for (int i = 0; i < enemiesProperties.EnemiesAmount; i++)
        {
            CreateEnemy(enemiesProperties.CurrentEnemy);
            yield return new WaitForSeconds(enemiesProperties.TimeBetweenSpawns);
        }
    }

    private void CreateEnemy(Enemy enemy)
    {
        GameObject currentEnemy = Instantiate(_enemyPrefab);
        currentEnemy.transform.parent = transform;
        currentEnemy.GetComponent<SpriteRenderer>().sprite = enemy.EnemyImage;
        currentEnemy.GetComponent<Animator>().runtimeAnimatorController = enemy.EnemyAnimator;
        currentEnemy.GetComponent<EnemyController>().SetСharacteristics(enemy,_pathCreator.PathPoints);
    }
}