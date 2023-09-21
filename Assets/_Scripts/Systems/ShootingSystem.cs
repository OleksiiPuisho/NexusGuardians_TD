using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootingSystem
{
    void ShootingHandler(Transform targetAttack, GameObject bulletPrefab);
}
public class ShootingSystem : IShootingSystem
{
    private IRotateSystem _rotateSystem;
    public void ShootingHandler(Transform targetAttack, GameObject bulletPrefab)
    {
        
    }

    private IEnumerator ReloadGun(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
