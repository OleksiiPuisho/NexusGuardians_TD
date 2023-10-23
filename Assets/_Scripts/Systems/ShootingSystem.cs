using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootingSystem
{
    void Shooting(Transform targetAttack, GameObject bulletPrefab, Transform parent, Vector2 firingSpread);
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

    public void Shooting(Transform targetAttack, GameObject bulletPrefab, Transform parent, Vector2 firingSpread)
    {
        if (targetAttack == null)
            return;

        var bulletObject = PoolObjects.GetObject(bulletPrefab);
        var bullet = bulletObject.GetComponent<BulletBehaviorBase>();
        bullet.SetProperties(_damage, _speedBullet);

        Vector3 angle = parent.rotation.eulerAngles;
        angle.x += Random.Range(firingSpread.x, firingSpread.y);
        angle.y += Random.Range(firingSpread.x, firingSpread.y);
        bulletObject.transform.SetPositionAndRotation(parent.position, Quaternion.Euler(angle));
        bulletObject.SetActive(true);
    }
}
