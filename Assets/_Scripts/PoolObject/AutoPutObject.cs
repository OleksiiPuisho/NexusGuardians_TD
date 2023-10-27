using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPutObject : MonoBehaviour
{
    [SerializeField] private float _autoPutDelay;

    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_autoPutDelay);
    }
    IEnumerator AutoPutDelay()
    {
        yield return _waitForSeconds;
        PoolObjects.PutObject(gameObject);
    }
    private void OnEnable()
    {
        StartCoroutine(AutoPutDelay());
    }
}
