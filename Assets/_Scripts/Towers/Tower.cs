using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerSystems;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private Transform _axsisTurret;
    [SerializeField] private Transform _turret;
    [SerializeField] private Light _lightRadius;

    [SerializeField] private TowerData _towerData;

    private IRotateTower _rotationSystem;
    private ISearchSystem _searchSystem;

    [SerializeField] private Transform _testBase;
    [SerializeField] private Transform _testTarget;
    [SerializeField] private List<Enemy> _enemies = new();
    void Awake()
    {
        PrepareTowerSystems();
    }
    void Update()
    {
        if (_testTarget != null)
            _rotationSystem.RotationHandler(_testTarget);
    }

    protected virtual void PrepareTowerSystems()
    {
        _rotationSystem = new RotationTowerSystem(_towerData.SpeedRotate, _axsisTurret, _turret);
        _searchSystem = new SearchTowerSystem(transform.position, _testBase.position, _towerData.Radius);
    }
    [ContextMenu("ToTower")]
    protected virtual void NearestToTower()
    {
        _searchSystem.SetSearchingType(SearchingType.NearestToTower);
        var enemy = _searchSystem.GetTarget(_enemies);
        Debug.Log(enemy.name);
    }
    [ContextMenu("ToBase")]
    protected virtual void NearestToBase()
    {
        _searchSystem.SetSearchingType(SearchingType.NearestToBase);
        var enemy = _searchSystem.GetTarget(_enemies);
        Debug.Log(enemy.name);
    }
    [ContextMenu("View Radius")]
    protected virtual void ViewRadius()
    {
        _lightRadius.gameObject.SetActive(true);
        _lightRadius.range = Mathf.Lerp(0, _towerData.Radius * 2f, 3f);
        _lightRadius.transform.localPosition = new(0.0f, Mathf.Lerp(0, _towerData.Radius, 3f) - 1f, 0.0f);
        //if (_lightRadius.range < _towerData.Radius * 2 && _lightRadius.transform.localPosition.y < _towerData.Radius - 1f)
            //ViewRadius();
    }
}

[System.Serializable] 
public class TowerData
{
    [SerializeField] private float _radius;
    [SerializeField] private float _speedRotate;

    public float Radius => _radius;
    public float SpeedRotate => _speedRotate;
}
