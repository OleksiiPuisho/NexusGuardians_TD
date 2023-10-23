using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBehaviorBase : MonoBehaviour
{
    [SerializeField] protected float _timeToDeactivationBullet;

    private Vector2 _damage;
    private float _speedBullet;

    public void SetProperties(Vector2 damage, float speed)
    {
        _damage = damage;
        _speedBullet = speed;
    }

    private void Update()
    {
        MovementHandler();
    }
    protected virtual void MovementHandler()
    {
        transform.Translate(transform.forward * (_speedBullet * Time.deltaTime), Space.World);
    }
    private void AutoPut()
    {
        PoolObjects.PutObject(gameObject);
    }

    IEnumerator AutoPutDelay()
    {
        yield return new WaitForSeconds(_timeToDeactivationBullet);
        AutoPut();
    }

    private void OnEnable()
    {
        StartCoroutine(AutoPutDelay());
    }
    private void OnCollisionEnter(Collision collision)
    {
        AutoPut();
    }
}
