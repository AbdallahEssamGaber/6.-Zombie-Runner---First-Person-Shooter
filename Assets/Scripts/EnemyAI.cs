using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;

    NavMeshAgent navMeshAgent;
    float rangeToEnemy = Mathf.Infinity;    //to not causing problems in the future "PROGRAMMING"

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        rangeToEnemy = Vector3.Distance(target.position, transform.position);
        if(rangeToEnemy <= chaseRange)
        {
            navMeshAgent.SetDestination(target.position);
        }

    }
}
