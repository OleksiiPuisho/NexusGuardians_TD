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

    private float _speedVisible;

    void Awake()
    {
        EventAggregator.Subscribe<DeselectedAll>(DeselectHandler);
        EventAggregator.Subscribe<SelectedObjectEvent>(SelectedObjecttHandler);

        _towerData = _towerComponent.GetTowerData();

        ResetLightRadius();
    }
    private void Update()
    {
        if (_isVisible)
            ShowRadius();
        else if (_isUnvisible)
            HideRadius();

    }

    private void ResetLightRadius()
    {
        _lightRadius.transform.localPosition = new(0.0f, _towerData.Radius - 1f, 0.0f);
        _lightRadius.range = _towerData.Radius;
        _lightRadius.intensity = (_towerData.Radius / 2f) * _multiplerIntensity;
        _speedVisible = _towerData.Radius + _speedVisibleOffset;

        _lightRadius.gameObject.SetActive(false);
        _isVisible = false;
    }
    private void ShowRadius()
    {
        if (_lightRadius.gameObject.activeSelf == false)
            _lightRadius.gameObject.SetActive(true);

        _lightRadius.range += _speedVisible * Time.deltaTime;

        if (_lightRadius.range > _towerData.Radius * 2f - 1f)
        {
            _lightRadius.range = _towerData.Radius * 2f;
            _isVisible = false;
            _isUnvisible = false;
        }
    }
    private void HideRadius()
    {
        if (IsSelected)
            IsSelected = false;

        _lightRadius.range -= _speedVisible * Time.deltaTime;

        if (_lightRadius.range < 1f)
        {
            ResetLightRadius();
            _isUnvisible = false;
        }
    }
    private void DeselectHandler(object sender, DeselectedAll eventData)
    {
        _isVisible = false;
        _isUnvisible = true;
    }
    private void SelectedObjecttHandler(object sender, SelectedObjectEvent eventData)
    {
        if (IsSelected && eventData.SelectedObject == this)
        {
            return;
        }
        _isUnvisible = true;
        _isVisible = false;
    }

    private void OnDestroy()
    {
        EventAggregator.Unsubscribe<DeselectedAll>(DeselectHandler);
        EventAggregator.Unsubscribe<SelectedObjectEvent>(SelectedObjecttHandler);
    }
}
