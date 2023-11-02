using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerSystems
{
    public class ShootingSystemGunTower : ShootingSystemBase
    {
        private List<Animator> _gunsAnimator = new();

        public ShootingSystemGunTower(Vector2 damage, float speedBullet, WaitForSeconds waitForSecondsDelay, WaitForSeconds waitForSecondsReloading, Transform[] bulletParents, TowerData towerData)
        {
            _damage = damage;
            _speedBullet = speedBullet;
            _waitForSecondsDelay = waitForSecondsDelay;
            _waitForSecondsReloading = waitForSecondsReloading;
            _bulletParents = bulletParents;
            _towerData = towerData;

            for (int i = 0; i < _bulletParents.Length; i++)
            {
                _gunsAnimator.Add(_bulletParents[i].parent.GetComponent<Animator>());
            }
        }

        public override IEnumerator ShootingDelay(Transform targetAttack)
        {
            IsShooting = true;
            if (_hasShooting == false)
            {
                yield return _waitForSecondsReloading;
                _hasShooting = true;
            }
            else
            {
                for (int i = 0; i < _bulletParents.Length; i++)
                {
                    _gunsAnimator[i].SetTrigger("IsShooting");
                    Shooting(targetAttack, _towerData.GetBullet(_towerData.BulletType), _bulletParents[i], _towerData.FiringSpread);
                    yield return _waitForSecondsDelay;
                }
                _hasShooting = false;
            }
            IsShooting = false;
        }
    }
}
