using System.Collections;
using System.Collections.Generic;
using TowerSystems;
using UnityEngine;

public class MachineGun : Tower
{
    protected override void PrepareTowerSystems()
    {
        _rotationSystem = new RotationSystem(_towerData.SpeedRotate, _towerData.SpeedBullet);
        _searchSystem = new SearchTowerSystem(_turret.position, _testBase.position, _towerData.Radius);
        _shootingSystem = new ShootingSystemMachineGun(_towerData.Damage, _towerData.SpeedBullet, _waitForSecondsDelay, _waitForSecondsReloading, _bulletParents, _towerData);

        _searchSystem.SetSearchingType(_towerData.SearchingType);
    }
}
