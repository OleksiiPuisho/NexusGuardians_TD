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
    [SerializeField] private int _indexSpawnrPoint;

    public int Count => _count;
    public GameObject PrefabEnemy => _prefabEnemy;
    public int IndexSpawnrPoint => _indexSpawnrPoint;
}
[System.Serializable]
public class Wave
{
    [SerializeField] private EnemyInstance[] _enemyInstances;
    [SerializeField] private int _rewardWave;
    public EnemyInstance[] EnemyInstances => _enemyInstances;
    public int RewardWave => _rewardWave;
}
