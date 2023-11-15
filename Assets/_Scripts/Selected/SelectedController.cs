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
        EventAggregator.Subscribe<DeselectedAllEvent>(delegate { _activeSelected = null; });
    }

    private void SetActiveSelected(object sender, SelectedObjectEvent eventData)
    {
        _activeSelected = eventData.SelectedObject;
    }
    private void OnDisable()
    {
        EventAggregator.Unsubscribe<SelectedObjectEvent>(SetActiveSelected);
        EventAggregator.Unsubscribe<DeselectedAllEvent>(delegate { _activeSelected = null; });
    }
}
