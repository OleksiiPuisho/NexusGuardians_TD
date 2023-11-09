using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Helpers.Events;

public class WaveController : MonoBehaviour
{
    public static List<Enemy> EnemiesGroundList = new List<Enemy>();
    public static List<Enemy> EnemiesAirList = new List<Enemy>();

    [SerializeField] private WaveScriptable _levelWaves;
    [SerializeField] private float _waveDuration;

    private Queue<GameObject> _enemiesSpawnLeft = new Queue<GameObject>();

    private bool _hasSpawn = false;
    private float _timeCounter;
    private int _currentWave = 0;

    private void Awake()
    {

    }

    private IEnumerator Spawn()
    {
        _timeCounter = 0;

        while (_timeCounter < _waveDuration)
        {
            float normalizedTime = _timeCounter / _waveDuration;
            ///
            _timeCounter += Time.deltaTime;
            yield return null;
        }
    }
    [ContextMenu("SpawnEnemyList")]
    private void PrepareEnemyList()
    {
        _enemiesSpawnLeft.Clear();

        for (int i = _levelWaves.Waves[_currentWave].EnemyInstances.Length; i > 0; i--)
        {
            for (int j = 0; j < _levelWaves.Waves[_currentWave].EnemyInstances[i].Count; j++)
            {
                _enemiesSpawnLeft.Enqueue(_levelWaves.Waves[_currentWave].EnemyInstances[j].PrefabEnemy);
            }
        }
        Debug.Log(_enemiesSpawnLeft.Count);
    }
}
