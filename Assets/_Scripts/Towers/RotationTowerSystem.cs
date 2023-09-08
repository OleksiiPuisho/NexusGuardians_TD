using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotateTower
{
    void RotationHandler(Transform target, Transform axsisTurret, Transform turret, float speedRotate);
}
public class RotationTowerSystem : IRotateTower
{

    public void RotationHandler(Transform target, Transform axsisTurret, Transform turret, float speedRotate)
    {
        Quaternion directionAxsis = Quaternion.LookRotation(target.position - axsisTurret.position);
        Quaternion clampedAxsis = new Quaternion(0.0f, directionAxsis.y, 0.0f, directionAxsis.w);
        
        Quaternion directionTurret = Quaternion.LookRotation(target.position - turret.position);
        Quaternion clampedTurret = Quaternion.Euler(directionTurret.eulerAngles.x, turret.localEulerAngles.y, turret.localEulerAngles.z);

        turret.localRotation = Quaternion.SlerpUnclamped(turret.localRotation, clampedTurret, speedRotate * Time.deltaTime);
        axsisTurret.rotation = Quaternion.SlerpUnclamped(axsisTurret.localRotation, clampedAxsis, speedRotate * Time.deltaTime);
    }
}
