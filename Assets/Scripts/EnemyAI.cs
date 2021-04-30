using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

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
        FaceTarget();
        GetComponent<Animator>().SetBool("attack", true);

    }


    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation , Time.deltaTime * turnSpeed);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
