using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveEffect : MonoBehaviour
{
    [SerializeField] private string _disolvePropertyName;
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] private Vector2 _propertyValue;
    [SerializeField] private float _duration;

    private float _timeCounter;
    private void Start()
    {
        StartCoroutine(Disolve());
    }

    private IEnumerator Disolve()
    {
        SetFloatPropertyMaterial(_propertyValue.x);
        _timeCounter = 0;
        while (_timeCounter < _duration)
        {
            var normalizedTime = _timeCounter / _duration;
            var propertyValue = Mathf.Lerp(_propertyValue.x, _propertyValue.y, normalizedTime);
            SetFloatPropertyMaterial(propertyValue);
            _timeCounter += Time.deltaTime;
            yield return null;
        }
        enabled = false;
    }

    private void SetFloatPropertyMaterial(float value)
    {
        for(int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.SetFloat(_disolvePropertyName, value);
        }
    }
}
