using UnityEngine;

[System.Serializable] 
public class TowerData
{
    [SerializeField] private float _radius;
    [SerializeField] private float _speedRotate;

    public float Radius => _radius;
    public float SpeedRotate => _speedRotate;
}
