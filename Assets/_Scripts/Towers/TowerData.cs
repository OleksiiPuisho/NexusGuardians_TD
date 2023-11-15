using System.Collections.Generic;
using TowerSystems;
using UnityEngine;

[System.Serializable] 
public class TowerData
{
    [SerializeField] private string _name;
    [SerializeField] private TowerType _towerType;
    [SerializeField] private float _upgradeStrenght;
    [SerializeField] private SearchingType _searchingType;
    [SerializeField] private AttackType _attackType;
    [SerializeField] private BulletType _bulletType;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _shootDetection;
    [SerializeField] private List<BulletData> _bulletDatas = new();

    public int Level;
    public float Radius;
    public float SpeedRotate;
    public Vector2 Damage;
    public float ReloadingSpeed;
    public float FiringSpread;

    public string Name => _name;
    public TowerType TowerType => _towerType;
    public float UpgradeStrenght => _upgradeStrenght;
    public SearchingType SearchingType => _searchingType;
    public AttackType AttackType => _attackType;
    public BulletType BulletType => _bulletType;
    public float ShootingDelay => _shootingDelay;
    public float SpeedBullet => _speedBullet;
    public float ShootDetection => _shootDetection;


    public GameObject GetBullet(BulletType bulletType)
    {
        return _bulletDatas.Find(bullet => bullet.BulletType == bulletType).BulletPrefab;
    }
}
public enum TowerType
{
    MachineGun,
    GunTower
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
