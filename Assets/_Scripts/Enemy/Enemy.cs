using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public float Health { get; }
    void SetHealth(float ammount);
}
public class Enemy : MonoBehaviour, IEnemy
{
    public float Health { get; private set; }

    public void SetHealth(float ammount)
    {
        Health += ammount;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
