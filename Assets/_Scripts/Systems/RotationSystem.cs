using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IRotateSystem
{
    Quaternion GetRotation(Transform rotationObject, Transform target);
    Quaternion GetRotation(Transform rotationObject, Transform target, bool enabledX, bool enabledY, bool enabledZ);
    bool LookToTarget(Transform fromObject, ref Transform target, float shootDetection);
}
public class RotationSystem : IRotateSystem
{
    private float _speedRotate;
    private float _speedBullet;

    public RotationSystem(float speedRotate, float speedBullet)
    {
        _speedRotate = speedRotate;
        _speedBullet = speedBullet;
    }

    public Quaternion GetRotation(Transform rotationObject, Transform target)
    {
        Quaternion result = Quaternion.SlerpUnclamped(rotationObject.rotation, RotationHandler(rotationObject, target.position), _speedRotate * Time.deltaTime);
        return result;
    }
    public Quaternion GetRotation(Transform rotationObject, Transform target, bool enabledX, bool enabledY, bool enabledZ)
    {
        Quaternion result = Quaternion.SlerpUnclamped(rotationObject.rotation, RotationHandler(rotationObject, GetLeadPoint(rotationObject.position, target)), _speedRotate * Time.deltaTime);

        if (enabledX == false)
            result = Quaternion.Euler(rotationObject.rotation.eulerAngles.x, result.eulerAngles.y, result.eulerAngles.z);
        if (enabledY == false)
            result = Quaternion.Euler(result.eulerAngles.x, rotationObject.rotation.eulerAngles.y, result.eulerAngles.z);
        if (enabledZ == false)
            result = Quaternion.Euler(result.eulerAngles.x, result.eulerAngles.y, rotationObject.rotation.eulerAngles.z);

        return result;
    }

    public bool LookToTarget(Transform fromObject, ref Transform target, float shootDetection)
    {
        if (Physics.Raycast(fromObject.position, fromObject.forward, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<Enemy>(out var enemy))
            {
                target = enemy.transform;
                return true;
            }
            if (hit.collider.transform == target || Vector3.Distance(GetLeadPoint(fromObject.position, target), hit.point) < shootDetection)
                return true;
        }
        return false;
    }

    private Vector3 GetLeadPoint(Vector3 from, Transform target)
    {
        float timeToTarget = Vector3.Distance(from, target.position) / _speedBullet;
        return target.position + target.GetComponent<NavMeshAgent>().velocity * timeToTarget;
    }

    private Quaternion RotationHandler(Transform rotationObject, Vector3 targetPoint)
    {
        Quaternion direction = Quaternion.LookRotation(targetPoint - rotationObject.position);
        Quaternion result = Quaternion.Euler(direction.eulerAngles.x, direction.eulerAngles.y, direction.eulerAngles.z);
        
        return result;
    }
}
