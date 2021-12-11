using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform tower;
    public Animator animator;
    public float 发呆 = 1f;
    private bool isRuning = false;//是否正在行军中

    void Start()
    {
        tower = GameObject.FindGameObjectWithTag("Tower").transform;
        Invoke("Run",发呆);
    }

    void Run() 
    {
        animator.SetBool("跑步",true);
        agent.SetDestination(tower.position);
        isRuning = true;
    }


    void Update()
    {
        if (isRuning&&!agent.pathPending&&agent.remainingDistance<1f)
        {
            Destroy(gameObject);
        }
    }
}
