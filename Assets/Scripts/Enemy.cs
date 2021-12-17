﻿using GK;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static WaveManager;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform tower;
    public Animator animator;
    public float 发呆 = 1f;
    private bool isRuning = false;//是否正在行军中

    [Header("血量")]
    public float HP = 3000f;
    [Header("攻击")]
    public float AT = 300f;
    public EnemyEvent OnEnemyHited = new EnemyEvent();

    [HideInInspector]
    public Wave wave; //我是属于谁的 （Wave）

    private float cached_HP;
    void Start()
    {
        cached_HP = HP;
        AT = Random.Range(200, 500);
        tower = GameObject.FindGameObjectWithTag("Tower").transform;
        Invoke("Run", 发呆);
    }

    void Run()
    {
        animator.SetBool("跑步", true);
        agent.SetDestination(tower.position);
        isRuning = true;
    }


    void Update()
    {
        if (isRuning && !agent.pathPending && agent.remainingDistance < 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{nameof(Enemy)}: { collision.collider.name}");
        var other = collision.collider.name;
        if (HP <= 0) return;

        if (other.Contains("Projectile"))
        {
            var 子弹 = collision.collider.GetComponent<Projectile>();
            HP -= 子弹.AT;

            if (HP <= 0f)
            {
                agent.isStopped = true;
                animator.SetTrigger("死亡");
                // Todo 消亡 ： Unity Shader 溶解
                //Destroy(gameObject,2f);// 延迟销毁自身
            }
            else
            {
                animator.SetTrigger("受伤害");
            }
            OnEnemyHited.Invoke(HP / cached_HP);
        }
        Debug.Log($"{nameof(Enemy)}: 血量 = {HP}");
    }

    private void OnDestroy()
    {
        wave.enemys.Remove(gameObject);
    }

    [Serializable]
    public class EnemyEvent : UnityEvent<float> { }
}
