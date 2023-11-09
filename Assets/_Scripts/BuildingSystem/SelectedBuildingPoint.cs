using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Helpers;
using Helpers.Events;

public interface IBuildingPoint
{
    Transform GetBuildingPosition();
}

public class SelectedBuildingPoint : SelectedObject, IBuildingPoint, IPointerClickHandler
{
    [SerializeField] private Transform _towerPosition;
    [SerializeField] private Animator _animator;

    public Transform GetBuildingPosition() => _towerPosition;

    private void Start()
    {
        EventAggregator.Subscribe<SelectedObjectEvent>(SelectedObjectHandler);
        EventAggregator.Subscribe<DeselectedAllEvent>(delegate { _animator.SetBool("IsActive", false); });
    }

    private void SelectedObjectHandler(object sender, SelectedObjectEvent eventData)
    {
        if (_isVisible)
        {
            if (eventData.SelectedObject == this)
                _animator.SetBool("IsActive", true);
            else
                _animator.SetBool("IsActive", false);
        }
    }

    private void OnDisable()
    {
        EventAggregator.Unsubscribe<SelectedObjectEvent>(SelectedObjectHandler);
        EventAggregator.Unsubscribe<DeselectedAllEvent>(delegate { _animator.SetBool("IsActive", false); });
    }
}
