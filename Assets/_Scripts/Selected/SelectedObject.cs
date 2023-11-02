using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Helpers;
using Helpers.Events;

public interface ISelectedObject
{
    public bool IsSelected { get; }
}
public abstract class SelectedObject : MonoBehaviour, ISelectedObject, IPointerClickHandler
{
    [SerializeField] protected TypeSelectedObject _typeSelectedObject;
    [SerializeField] private bool _selectedVisualization;
    protected bool _isVisible = false;
    protected bool _isUnvisible = false;

    public bool IsSelected { get; protected set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventAggregator.Post(this, new DeselectedAllEvent());
        IsSelected = true;
        if (_selectedVisualization) _isVisible = true;
        EventAggregator.Post(this, new SelectedObjectEvent() { TypeSelectedObject = _typeSelectedObject, SelectedObject = this });
    }
}

public enum TypeSelectedObject
{
    Tower,
    Enemy,
    MainBase,
    BuildTowerPoint
}
