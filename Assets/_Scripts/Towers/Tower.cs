using System.Collections.Generic;
using UnityEngine;
using TowerSystems;
using System.Linq;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected Transform _axsisTurret;
    [SerializeField] protected Transform _turret;
    [SerializeField] protected Transform[] _bulletParents;

    [SerializeField] protected TowerData _towerData;

    protected IRotateSystem _rotationSystem;
    protected ISearchSystem _searchSystem;
    protected IShootingSystem _shootingSystem;

    [SerializeField] protected Transform _testBase;//test
    protected Transform _target;
    [SerializeField] protected List<Enemy> _enemies = new();//test


    protected WaitForSeconds _waitForSecondsDelay;
    protected WaitForSeconds _waitForSecondsReloading;
    public TowerData GetTowerData() => _towerData;

    void Start()
    {
        _testBase = GameObject.Find("Nexsus").transform;//test
        Enemy[] enemy = (Enemy[])FindObjectsOfTypeAll(typeof(Enemy));//test
        _enemies = enemy.ToList();//test

        _waitForSecondsDelay = new WaitForSeconds(_towerData.ShootingDelay);
        _waitForSecondsReloading = new WaitForSeconds(_towerData.ReloadingSpeed);

        PrepareTowerSystems();
    }
    void Update()
    {        
        if (_target != null && _target.gameObject.activeSelf)
        {
            ShootingHandler();
        }
        else
        {
            _target = _searchSystem.GetTarget(_enemies);
        }
    }
    private void FixedUpdate()
    {
        if (_target != null && _target.gameObject.activeSelf)
        {
            _axsisTurret.rotation = _rotationSystem.GetRotation(_axsisTurret, _target, false, true, false);
            _turret.rotation = _rotationSystem.GetRotation(_turret, _target, true, false, false);
        }
    }

    protected virtual void ShootingHandler()
    {       
        if (_rotationSystem.LookToTarget(_turret, ref _target, _towerData.ShootDetection) && _shootingSystem.IsShooting == false)
        {
            StartCoroutine(_shootingSystem.ShootingDelay(_target));
        }

        var distance = (_target.position - transform.position).sqrMagnitude;

        if (distance > _towerData.Radius * _towerData.Radius)
        {
            _target = null;
        }
    }

    protected abstract void PrepareTowerSystems();
}
