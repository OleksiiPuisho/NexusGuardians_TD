using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Helpers;
using Helpers.Events;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IDamageble
{
    public EnemyType EnemyType;

    [SerializeField] private float _testHealth;
    [SerializeField] private Slider _testHealthSlider;
    [SerializeField] private Transform _testDestination;


    private NavMeshAgent _agent;
    public float Health => _testHealth;

    public void SetHealth(float ammount)
    {
        if (_testHealthSlider.gameObject.activeSelf == false)
            _testHealthSlider.gameObject.SetActive(true);

        _testHealth += ammount;
        _testHealthSlider.value = _testHealth;
        if (_testHealth <= 0)
        {
            EventAggregator.Post(this, new EnergyUpdateEvent() { MoneyAmmount = 30 });// add death prise!!!!!!!
            PoolObjects.PutObject(gameObject);
        }
    }
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _testDestination = GameObject.Find("Nexsus").transform;
    }
    void FixedUpdate()
    {
        _agent.SetDestination(_testDestination.position);
    }
    private void OnEnable()
    {
        _testHealth = 10f;
        _testHealthSlider.value = 10f;
    }
}

public enum EnemyType { Ground, Air}
