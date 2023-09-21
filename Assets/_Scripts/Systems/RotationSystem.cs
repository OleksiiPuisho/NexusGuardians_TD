using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotateSystem
{
    Quaternion GetRotation(Transform rotationObject, Transform target);
    Quaternion GetRotation(Transform rotationObject, Transform target, bool enabledX, bool enabledY, bool enabledZ);
    bool LookToTarget(Transform fromObject, ref Transform target);
}
public class RotationSystem : IRotateSystem
{
    private float _speedRotate;

    public RotationSystem(float speedRotate)
    {
        _speedRotate = speedRotate;
    }

    public Quaternion GetRotation(Transform rotationObject, Transform target)
    {
        Quaternion result = Quaternion.SlerpUnclamped(rotationObject.rotation, RotationHandler(rotationObject, target), _speedRotate * Time.deltaTime);
        return result;
    }
    public Quaternion GetRotation(Transform rotationObject, Transform target, bool enabledX, bool enabledY, bool enabledZ)
    {
        Quaternion result = Quaternion.SlerpUnclamped(rotationObject.rotation, RotationHandler(rotationObject, target), _speedRotate * Time.deltaTime);

        if (enabledX == false)
            result = Quaternion.Euler(rotationObject.rotation.eulerAngles.x, result.eulerAngles.y, result.eulerAngles.z);
        if (enabledY == false)
            result = Quaternion.Euler(result.eulerAngles.x, rotationObject.rotation.eulerAngles.y, result.eulerAngles.z);
        if (enabledZ == false)
            result = Quaternion.Euler(result.eulerAngles.x, result.eulerAngles.y, rotationObject.rotation.eulerAngles.z);

        return result;
    }

    public bool LookToTarget(Transform fromObject, ref Transform target)
    {
        if (Physics.Raycast(fromObject.position, fromObject.forward, out RaycastHit hit))
        {
            if (hit.collider.transform == target)
            {
                return true;
            }
        }
        return false;
    }

    private Quaternion RotationHandler(Transform rotationObject, Transform target)
    {
        Quaternion direction = Quaternion.LookRotation(target.position - rotationObject.position);
        Quaternion result = Quaternion.Euler(direction.eulerAngles.x, direction.eulerAngles.y, direction.eulerAngles.z);
        
        return result;
    }
}
