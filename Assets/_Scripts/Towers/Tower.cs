using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerSystems;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private Transform _axsisTurret;
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform[] _bulletParents;

    [SerializeField] private TowerData _towerData;

    private IRotateSystem _rotationSystem;
    private ISearchSystem _searchSystem;
    private IShootingSystem _shootingSystem;

    [SerializeField] private Transform _testBase;//test
    private Transform _target;
    [SerializeField] private List<Enemy> _enemies = new();//test
    [SerializeField] private GameObject _bulletPrefab;

    private bool _hasShooting = false;
    private bool _isShooting = false;

    public TowerData GetTowerData() => _towerData;

    private WaitForSeconds _waitForSecondsDelay;
    private WaitForSeconds _waitForSecondsReloading;

    void Awake()
    {
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

    private void ShootingHandler()
    {
        _axsisTurret.rotation = _rotationSystem.GetRotation(_axsisTurret, _target, false, true, false);
        _turret.rotation = _rotationSystem.GetRotation(_turret, _target, true, false, false);
        if (_rotationSystem.LookToTarget(_turret, ref _target, _towerData.ShootDetection) && _isShooting == false)
        {
            StartCoroutine(ShootingDelay());
        }
        if (Vector3.Distance(transform.position, _target.position) > _towerData.Radius)
        {
            _target = null;
        }
    }

    private IEnumerator ShootingDelay()
    {
        _isShooting = true;
        if(_hasShooting == false)
        {
            yield return _waitForSecondsReloading;
            _hasShooting = true;
        }
        else
        {
            for (int i = 0; i < _bulletParents.Length; i++)
            {
                _shootingSystem.Shooting(_target, _bulletPrefab, _bulletParents[i], _towerData.FiringSpread);
                yield return _waitForSecondsDelay;
            }
        }
        _isShooting = false;
    }

    protected virtual void PrepareTowerSystems()
    {
        _rotationSystem = new RotationSystem(_towerData.SpeedRotate, _towerData.SpeedBullet);
        _searchSystem = new SearchTowerSystem(transform.position, _testBase.position, _towerData.Radius);
        _shootingSystem = new ShootingSystem(_towerData.Damage, _towerData.SpeedBullet);
    }
}
