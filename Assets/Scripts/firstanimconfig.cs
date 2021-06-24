using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class firstanimconfig : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    [SerializeField] Transform target;
    [SerializeField] Transform targetPlayer;

    [SerializeField] GameObject[] nodestroyfahem;

    float distanceToTarget = Mathf.Infinity;
    float distanceToTargetPlayer = Mathf.Infinity;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        distanceToTargetPlayer = Vector3.Distance(targetPlayer.position, transform.position);

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            Arrived();
        }

    
    }

    public void GoThere()
    {
        GetComponent<Animator>().SetTrigger("runTrigger");
        navMeshAgent.SetDestination(target.position);
    }

    void Arrived()
    {
        GetComponent<Animator>().SetTrigger("idleTrigger");
    }

    public void BeforeScene()
    {
        GetComponent<Animator>().SetTrigger("runTrigger");
        navMeshAgent.SetDestination(targetPlayer.position);
        StartCoroutine(NextPlayScene());
    }

     IEnumerator NextPlayScene()
    {
        yield return new WaitForSeconds(.6f);
        foreach(GameObject gameObject in nodestroyfahem)
        {
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.LoadSceneAsync("Game 2");

    }
}
