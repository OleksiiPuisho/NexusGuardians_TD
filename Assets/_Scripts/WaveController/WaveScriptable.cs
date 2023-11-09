using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveLevel_", menuName = "Create/Wave")]
public class WaveScriptable : ScriptableObject
{
    [SerializeField] private Wave[] _waves;
    public Wave[] Waves => _waves;
}
[System.Serializable]
public class EnemyInstance
{
    [SerializeField] private int _count;
    [SerializeField] private GameObject _prefabEnemy;

    public int Count => _count;
    public GameObject PrefabEnemy => _prefabEnemy;
}
[System.Serializable]
public class Wave
{
    [SerializeField] private EnemyInstance[] _enemyInstances;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _rewardWave;
    public EnemyInstance[] EnemyInstances => _enemyInstances;
    public Transform[] SpawnPoints => _spawnPoints;
    public int RewardWave => _rewardWave;
}
