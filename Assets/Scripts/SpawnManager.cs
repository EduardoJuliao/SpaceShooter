using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _secondsToSpawn;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            var newX = Random.Range(Boundries.MinX, Boundries.MaxX);
            Instantiate(_enemyPrefab, new Vector3(newX, Boundries.MaxY + 2f, 0), Quaternion.identity);
            
            yield return new WaitForSeconds(_secondsToSpawn);
        }
    }
}
