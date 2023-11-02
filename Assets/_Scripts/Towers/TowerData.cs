using System.Collections.Generic;
using TowerSystems;
using UnityEngine;

[System.Serializable] 
public class TowerData
{
    [SerializeField] private string _name;
    [SerializeField] private SearchingType _searchingType;
    [SerializeField] private AttackType _attackType;
    [SerializeField] private BulletType _bulletType;
    [SerializeField] private float _radius;
    [SerializeField] private float _speedRotate;
    [SerializeField] private Vector2 _damage;
    [SerializeField] private float _reloadingSpeed;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _shootDetection;
    [SerializeField] private float _firingSpread;
    [SerializeField] private List<BulletData> _bulletDatas = new();

    public string Name => _name;
    public SearchingType SearchingType => _searchingType;
    public AttackType AttackType => _attackType;
    public BulletType BulletType => _bulletType;
    public float Radius => _radius;
    public float SpeedRotate => _speedRotate;
    public Vector2 Damage => _damage;
    public float ReloadingSpeed => _reloadingSpeed;
    public float ShootingDelay => _shootingDelay;
    public float SpeedBullet => _speedBullet;
    public float ShootDetection => _shootDetection;
    public float FiringSpread => _firingSpread;

    public GameObject GetBullet(BulletType bulletType)
    {
        return _bulletDatas.Find(bullet => bullet.BulletType == bulletType).BulletPrefab;
    }

}

public enum AttackType
{
    Ground,
    Air,
    GroundAir
}
public enum BulletType
{
    Incendiary,
    ArmorPiercing,
    Disintegrating
}

[System.Serializable]
public class BulletData
{
    [SerializeField] private BulletType _bulletType;
    [SerializeField] private GameObject _bulletPrefab;

    public BulletType BulletType => _bulletType;
    public GameObject BulletPrefab => _bulletPrefab;
}
