using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class BulletBehaviorBase : MonoBehaviour
{
    [SerializeField] private VisualEffect _shootParticle;
    [SerializeField] private VisualEffect _hitParticleMetal;
    [SerializeField] private VisualEffect _hitParticleGround;

    [SerializeField] private string _groundLayerName;

    [SerializeField] protected float _timeToDeactivationBullet;

    private Vector2 _damage;
    private float _speedBullet;

    private WaitForSeconds _waitForSeconds;

    public void SetProperties(Vector2 damage, float speed)
    {
        _damage = damage;
        _speedBullet = speed;
    }

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_timeToDeactivationBullet);
    }

    private void FixedUpdate()
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
        yield return _waitForSeconds;
        AutoPut();
    }

    private void OnEnable()
    {
        CreateParticle(_shootParticle);
        StartCoroutine(AutoPutDelay());
    }

    private void CreateParticle(VisualEffect particle)
    {
        GameObject shotParticle = PoolObjects.GetObject(particle.gameObject);
        shotParticle.transform.SetPositionAndRotation(transform.position, transform.rotation);
        shotParticle.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageble>(out var damageble))
            damageble.SetHealth(-Random.Range(_damage.x, _damage.y));

        if (collision.gameObject.layer == LayerMask.NameToLayer(_groundLayerName))
            CreateParticle(_hitParticleGround);
        else
            CreateParticle(_hitParticleMetal);
        AutoPut();
    }
}
