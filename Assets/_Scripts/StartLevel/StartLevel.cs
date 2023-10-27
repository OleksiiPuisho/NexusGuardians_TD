using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private List<InitializeData> _initializeDatas = new();
    [SerializeField] private Transform _globalParent;

    void Awake()
    {
        PoolObjects.ClearAllOnScene();
        InitializeObjects();
    }

    private void InitializeObjects()
    {
        PoolObjects.SetParentForObject(_globalParent);

        foreach (var data in _initializeDatas)
        {
            PoolObjects.InitializePool(data.Prefab, data.Count);
        }
    }
}

[System.Serializable]
public class InitializeData
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _count;

    public GameObject Prefab => _prefab;
    public int Count => _count;
}
