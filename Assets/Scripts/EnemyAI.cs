using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] GameObject gone;

    NavMeshAgent navMeshAgent;
    float rangeToEnemy = Mathf.Infinity;    //to not causing problems in the future "PROGRAMMING"

    bool test = false;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        rangeToEnemy = Vector3.Distance(target.position, transform.position);
        if(rangeToEnemy <= chaseRange)
        {
            test = true;
            gone.SetActive(false);

            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            if(test)
            {
                gone.SetActive(true);
            }
        }

    }
}
