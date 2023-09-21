using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerSystems;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private Transform _axsisTurret;
    [SerializeField] private Transform _turret;

    [SerializeField] private TowerData _towerData;

    private IRotateSystem _rotationSystem;
    private ISearchSystem _searchSystem;
    private IShootingSystem _shootingSystem;

    [SerializeField] private Transform _testBase;
    private Transform _target;
    [SerializeField] private List<Enemy> _enemies = new();

    public TowerData GetTowerData() => _towerData;

    void Awake()
    {
        PrepareTowerSystems();
    }
    void Update()
    {        
        if (_target != null && _target.gameObject.activeSelf)
        {
            _axsisTurret.rotation = _rotationSystem.GetRotation(_axsisTurret, _target, false, true, false);
            _turret.rotation = _rotationSystem.GetRotation(_turret, _target, true, false, false);
            if(_rotationSystem.LookToTarget(_turret, ref _target))
            {
                Debug.Log("Is ok");
            }
        }
        else
        {
            _target = _searchSystem.GetTarget(_enemies);
        }
    }

    protected virtual void PrepareTowerSystems()
    {
        _rotationSystem = new RotationSystem(_towerData.SpeedRotate);
        _searchSystem = new SearchTowerSystem(transform.position, _testBase.position, _towerData.Radius);
        _shootingSystem = new ShootingSystem();
    }
}
