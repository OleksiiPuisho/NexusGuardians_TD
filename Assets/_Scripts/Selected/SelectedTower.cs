using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Helpers.Events;

public class SelectedTower : SelectedObject
{
    [SerializeField] private Light _lightRadius;
    [SerializeField] private Tower _towerComponent;
    [SerializeField] private float _speedVisibleOffset;
    private TowerData _towerData;
    private float _multiplerIntensity = 1000f;

    private WaitForSeconds _waitForSeconds;

    void Start()
    {
        _towerData = _towerComponent.GetTowerData();
        ResetLightRadius();
        _waitForSeconds = new WaitForSeconds(_speedVisibleOffset);

        EventAggregator.Subscribe<DeselectedAllEvent>(DeselectHandler);
        EventAggregator.Subscribe<SelectedObjectEvent>(SelectedObjectHandler);
    }

    private void ResetLightRadius()
    {
        _lightRadius.transform.localPosition = new(0.0f, _towerData.Radius - 1f, 0.0f);
        _lightRadius.range = _towerData.Radius;
        _lightRadius.intensity = (_towerData.Radius / 2f) * _multiplerIntensity;

        _lightRadius.enabled = false;
        _isVisible = false;
    }
    private void DeselectHandler(object sender, DeselectedAllEvent eventData)
    {
        _isVisible = false;
        _isUnvisible = true;
        StartCoroutine(VisualizationRadiusDelay());
    }
    private void SelectedObjectHandler(object sender, SelectedObjectEvent eventData)
    {
        if (eventData.SelectedObject == this)
        {
            _isUnvisible = false;
            _isVisible = true;
        }
        else
        {
            _isVisible = false;
            _isUnvisible = true;
        }
            StartCoroutine(VisualizationRadiusDelay());
    }

    private IEnumerator VisualizationRadiusDelay()
    {
        if(_isVisible)
        {
            if (_lightRadius.enabled == false)
                _lightRadius.enabled = true;

            _lightRadius.range += _towerData.Radius * Time.deltaTime;

            if (_lightRadius.range > _towerData.Radius * 2f - 1f)
            {
                _lightRadius.range = _towerData.Radius * 2f;
                _isVisible = false;
                _isUnvisible = false;
            }
            else
            {
                yield return _waitForSeconds;
                StartCoroutine(VisualizationRadiusDelay());
            }
        }
        else if (_isUnvisible)
        {
            if (IsSelected)
                IsSelected = false;

            _lightRadius.range -= _towerData.Radius * Time.deltaTime;

            if (_lightRadius.range < 1f)
            {
                ResetLightRadius();
                _isUnvisible = false;
            }
            else
            {
                yield return _waitForSeconds;
                StartCoroutine(VisualizationRadiusDelay());
            }
        }
    }

    private void OnDestroy()
    {
        EventAggregator.Unsubscribe<DeselectedAllEvent>(DeselectHandler);
        EventAggregator.Unsubscribe<SelectedObjectEvent>(SelectedObjectHandler);
    }
}
