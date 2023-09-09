using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Helpers;
using Helpers.Events;

public abstract class SelectedObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected TypeSelectedObject _typeSelectedObject;
    [SerializeField] private bool _selectedVisualization;
    protected bool _isSelected;
    protected bool _isVisible = false;
    protected bool _isUnvisible = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        EventAggregator.Post(this, new SelectedObjectEvent() { TypeSelectedObject = _typeSelectedObject});
        _isSelected = true;
        if (_selectedVisualization) _isVisible = true;
    }
}

public enum TypeSelectedObject
{
    Tower,
    Enemy,
    MainBase
}
