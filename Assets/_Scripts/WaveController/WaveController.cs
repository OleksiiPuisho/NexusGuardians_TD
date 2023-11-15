using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Helpers;
using Helpers.Events;

public class WaveController : MonoBehaviour
{
    public static List<Enemy> EnemiesGroundList = new List<Enemy>();
    public static List<Enemy> EnemiesAirList = new List<Enemy>();

    [SerializeField] private WaveScriptable _levelWaves;
    [SerializeField] private Transform[] _spawners;
    [SerializeField] private float _waveDuration;
    [SerializeField] private Vector2 _delaySpawn;

    private Queue<EnemyInstance> _enemiesSpawnLeft = new Queue<EnemyInstance>();

    private float _timeCounter;
    private int _currentWave = 0;

    private void Awake()
    {
        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        _timeCounter = 0;

        while (_timeCounter < _waveDuration)
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter >= _waveDuration)
            {
                PrepareEnemyList();
                StartCoroutine(SpawnEnemy());
            }
            else
            {               
                yield return null;
            }
        }
    }
    private void PrepareEnemyList()
    {
        _enemiesSpawnLeft.Clear();

        for (int i = 0; i < _levelWaves.Waves[_currentWave].EnemyInstances.Length; i++)
        {
            for (int j = 0; j < _levelWaves.Waves[_currentWave].EnemyInstances[i].Count; j++)
            {
                _enemiesSpawnLeft.Enqueue(_levelWaves.Waves[_currentWave].EnemyInstances[i]);
            }
        }
    }
    private IEnumerator SpawnEnemy()
    {
        while(_enemiesSpawnLeft.Count > 0)
        {
            GameObject enemyObject = PoolObjects.GetObject(_enemiesSpawnLeft.Peek().PrefabEnemy);
            Enemy enemy = enemyObject.GetComponent<Enemy>(); 

            var spawnPoint = _spawners[_enemiesSpawnLeft.Peek().IndexSpawnrPoint];
            enemyObject.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

            if (enemy.EnemyType == EnemyType.Ground)
                EnemiesGroundList.Add(enemy);
            else
                EnemiesAirList.Add(enemy);

            enemyObject.SetActive(true);
            _enemiesSpawnLeft.Dequeue();

            yield return new WaitForSeconds(Random.Range(_delaySpawn.x, _delaySpawn.y));
        }
        yield return null;
    }
}
