using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerSystems
{
    public interface IShootingSystem
    {
        bool IsShooting { get; }
        IEnumerator ShootingDelay(Transform targetAttack);
    }
    public abstract class ShootingSystemBase : IShootingSystem
    {
        protected Vector2 _damage;
        protected float _speedBullet;

        protected Transform[] _bulletParents;

        protected TowerData _towerData;

        protected bool _hasShooting = false;

        protected WaitForSeconds _waitForSecondsDelay;
        protected WaitForSeconds _waitForSecondsReloading;

        public bool IsShooting { get; protected set; } = false;

        public virtual IEnumerator ShootingDelay(Transform targetAttack)
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
                    Shooting(targetAttack, _towerData.GetBullet(_towerData.BulletType), _bulletParents[i], _towerData.FiringSpread);
                    yield return _waitForSecondsDelay;
                }
                _hasShooting = false;
            }
            IsShooting = false;
        }

        protected virtual void Shooting(Transform targetAttack, GameObject bulletPrefab, Transform parent, float firingSpread)
        {
            if (targetAttack == null)
                return;
            var bulletObject = PoolObjects.GetObject(bulletPrefab);
            var bullet = bulletObject.GetComponent<BulletBehaviorBase>();
            bullet.SetProperties(_damage, _speedBullet);

            Vector3 angle = parent.rotation.eulerAngles;
            angle.x += Random.Range(-firingSpread, firingSpread);
            angle.y += Random.Range(-firingSpread, firingSpread);
            bulletObject.transform.SetPositionAndRotation(parent.position, Quaternion.Euler(angle));
            bulletObject.SetActive(true);
        }

    }
}
