using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IDamageble
{
    public float Health { get; }
    void SetHealth(float ammount);
}
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float _testHealth;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _testDestination;
    public float Health => _testHealth;

    public void SetHealth(float ammount)
    {
        _testHealth += ammount;
        if (_testHealth <= 0)
            PoolObjects.PutObject(gameObject);
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        _agent.SetDestination(_testDestination.position);
    }
}
