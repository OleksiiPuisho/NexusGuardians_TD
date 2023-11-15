using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Helpers.Events;

public class MainBase : MonoBehaviour, IDamageble
{
    public static MainBase Instance;
    public float Health { get; private set; }

    public void SetHealth(float ammount)
    {
        Health += ammount;

        if (Health <= 0)
            EventAggregator.Post(this, new GameOverEvent());
    }

    void Awake()
    {
        Instance = this;
    }
}
