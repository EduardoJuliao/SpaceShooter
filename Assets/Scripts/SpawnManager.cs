using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _secondsToSpawn;
    [SerializeField] private GameObject _enemyContainer;
    
    [SerializeField] private GameObject _powerUpContainer;
    [SerializeField] private GameObject[] _powerUps;

    private bool _stopSpawning = false;
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnEnemies()
    {
        while (!_stopSpawning)
        {
            var newX = Random.Range(Boundries.MinX, Boundries.MaxX);
            var newEnemy = Instantiate(_enemyPrefab, new Vector3(newX, Boundries.MaxY + 2f, 0), Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(_secondsToSpawn);
        }
    }

    IEnumerator SpawnPowerUps()
    {
        

        while (!_stopSpawning)
        {
            var index = Random.Range(0, _powerUps.Length);
            var powerUp = _powerUps[index];
            var newX = Random.Range(Boundries.MinX, Boundries.MaxX);
            var newEnemy = Instantiate(powerUp, new Vector3(newX, Boundries.MaxY + 2f, 0), Quaternion.identity);
            newEnemy.transform.SetParent(_powerUpContainer.transform);
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
