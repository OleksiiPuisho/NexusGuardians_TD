using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Helpers.Events;

public class LevelObserver : MonoBehaviour
{
    [SerializeField] private int _energy;

    public static LevelObserver Instance;

    public int Energy => _energy;

    private void Start()
    {
        Instance = this;

        EventAggregator.Post(this, new EnergyUpdateUIEvent() { MoneyAmmount = _energy });

        EventAggregator.Subscribe<EnergyUpdateEvent>(UpdateEnergy);
    }

    private void UpdateEnergy(object sender, EnergyUpdateEvent eventData)
    {
        _energy += eventData.MoneyAmmount;
        EventAggregator.Post(this, new EnergyUpdateUIEvent() { MoneyAmmount = _energy });
    }
}
