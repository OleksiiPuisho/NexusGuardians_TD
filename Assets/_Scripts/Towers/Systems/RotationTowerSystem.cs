using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotateTower
{
    void RotationHandler(Transform target);
}
public class RotationTowerSystem : IRotateTower
{
    private Transform _axsisTurret;
    private Transform _turret;
    private float _speedRotate;

    public RotationTowerSystem(Transform axsisTurret, Transform turret, float speedRotate)
    {
        _axsisTurret = axsisTurret;
        _turret = turret;
        _speedRotate = speedRotate;
    }

    public void RotationHandler(Transform target)
    {
        
        Quaternion clampedAxsis = new Quaternion(0.0f, LookDirectionHandler(target, _axsisTurret).y, 0.0f, LookDirectionHandler(target, _axsisTurret).w);
        Quaternion clampedTurret = Quaternion.Euler(LookDirectionHandler(target, _turret).eulerAngles.x, _turret.localEulerAngles.y, _turret.localEulerAngles.z);

        _turret.localRotation = Quaternion.SlerpUnclamped(_turret.localRotation, clampedTurret, _speedRotate * Time.deltaTime);
        _axsisTurret.rotation = Quaternion.SlerpUnclamped(_axsisTurret.localRotation, clampedAxsis, _speedRotate * Time.deltaTime);
    }

    private Quaternion LookDirectionHandler(Transform toTarget, Transform obj)
    {
        Quaternion direction = Quaternion.LookRotation(toTarget.position - obj.position);
        return direction;
    }
}
