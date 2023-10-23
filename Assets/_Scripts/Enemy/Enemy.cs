using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemy
{
    public float Health { get; }
    void SetHealth(float ammount);
}
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _testDestination;
    public float Health { get; private set; }

    public void SetHealth(float ammount)
    {
        Health += ammount;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        _agent.SetDestination(_testDestination.position);
    }
}
