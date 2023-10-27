using UnityEngine;

[System.Serializable] 
public class TowerData
{
    [SerializeField] private float _radius;
    [SerializeField] private float _speedRotate;
    [SerializeField] private Vector2 _damage;
    [SerializeField] private float _reloadingSpeed;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _shootDetection;
    [SerializeField] private float _firingSpread;

    public float Radius => _radius;
    public float SpeedRotate => _speedRotate;
    public Vector2 Damage => _damage;
    public float ReloadingSpeed => _reloadingSpeed;
    public float ShootingDelay => _shootingDelay;
    public float SpeedBullet => _speedBullet;
    public float ShootDetection => _shootDetection;
    public float FiringSpread => _firingSpread;
}
