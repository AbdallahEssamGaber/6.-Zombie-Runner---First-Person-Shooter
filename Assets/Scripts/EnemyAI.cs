using System;
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
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        rangeToEnemy = Vector3.Distance(target.position, transform.position);


        if (isProvoked)
        {
            EngageTarget();
        }
        else if(rangeToEnemy <= chaseRange)
        {
            isProvoked = true;
            
        }
    

    }

    void EngageTarget()
    {
        if (rangeToEnemy >= navMeshAgent.stoppingDistance)
        {
            navMeshAgent.SetDestination(target.position);
        }
        else if (rangeToEnemy >= navMeshAgent.stoppingDistance)
        {
            print("hi");
        }

    
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
