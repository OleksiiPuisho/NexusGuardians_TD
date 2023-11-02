using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Helpers.Events;

public class SelectedController : MonoBehaviour
{
    private SelectedObject _activeSelected;

    public SelectedObject GetActiveSelected() => _activeSelected;
    private void Awake()
    {
        EventAggregator.Subscribe<SelectedObjectEvent>(SetActiveSelected);
    }

    private void SetActiveSelected(object sender, SelectedObjectEvent eventData)
    {
        _activeSelected = eventData.SelectedObject;
    }
    private void OnDisable()
    {
        EventAggregator.Unsubscribe<SelectedObjectEvent>(SetActiveSelected);
    }
}
