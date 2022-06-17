using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MissileBoss : Bullet
{
    // Start is called before the first frame update
    public Transform target;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        _navMeshAgent.SetDestination(target.position);
    }
}
