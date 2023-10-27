using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootingSystem
{
    void Shooting(Transform targetAttack, GameObject bulletPrefab, Transform parent, float firingSpread);
}
public class ShootingSystem : IShootingSystem
{
    private Vector2 _damage;
    private float _speedBullet;

    public ShootingSystem(Vector2 damage, float speedBullet)
    {
        _damage = damage;
        _speedBullet = speedBullet;
    }

    public void Shooting(Transform targetAttack, GameObject bulletPrefab, Transform parent, float firingSpread)
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
