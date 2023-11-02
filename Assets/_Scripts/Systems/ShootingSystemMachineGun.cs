using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerSystems
{
    public class ShootingSystemMachineGun : ShootingSystemBase
    {
       public ShootingSystemMachineGun(Vector2 damage, float speedBullet, WaitForSeconds waitForSecondsDelay, WaitForSeconds waitForSecondsReloading, Transform[] bulletParents, TowerData towerData)
        {
            _damage = damage;
            _speedBullet = speedBullet;
            _waitForSecondsDelay = waitForSecondsDelay;
            _waitForSecondsReloading = waitForSecondsReloading;
            _bulletParents = bulletParents;
            _towerData = towerData;
        }
    }
}
