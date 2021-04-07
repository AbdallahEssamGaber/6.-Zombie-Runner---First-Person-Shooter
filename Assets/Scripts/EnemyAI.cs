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
            isProvoked = false;
        }
        else if(rangeToEnemy <= chaseRange)
        {
            isProvoked = true;

        }
        else
        {
            GetComponent<Animator>().ResetTrigger("move");
            GetComponent<Animator>().SetTrigger("Idle");
        }



    }

    void EngageTarget()
    {
        GetComponent<Animator>().ResetTrigger("Idle");

        if (rangeToEnemy >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (rangeToEnemy <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }


    }
    void ChaseTarget()
    {
        GetComponent<Animator>().SetTrigger("move");
        GetComponent<Animator>().SetBool("attack", false);
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);

    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
