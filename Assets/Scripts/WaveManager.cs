using Malee.List;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.Events;

/// <summary>
/// 进攻波次管理器
/// <para>1. 管理进攻波次，分几波</para>
/// 2. 每次有啥怪，出生间隔
/// <para>3. 记录出生点位</para>
/// 
/// </summary>
public class WaveManager : MonoBehaviour
{
    [Header("出生点位"), Reorderable]
    public TransformList location;
    [Header("配置敌人生成的波次"), Reorderable]
    public WaveArray waves;

    [Header("当所有的波次都生成完毕时执行的事件")]
    public WaveEvent OnAllWaveFinished = new WaveEvent();
    Coroutine enemyCreateAction; //敌人生成的行为载体


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCreateEnemy();
        }
    }

    IEnumerator CreateEnemy()
    {
        foreach (var item in waves)
        {
            if (!item.enable) continue;
            for (int i = 0; i < item.num; i++)
            {

                Debug.Log($"{nameof(WaveManager)}: 波次名称 = {item.name}");

                var delay = new WaitForSeconds(item.interval); // 怪兽生成的间隔
                var index = Random.Range(0, item.prefabs.Length); // 随机一个怪兽的下标
                var enemy = Instantiate<GameObject>(item.prefabs[index]); // 承上启下，生成怪兽
              
                var index_location = Random.Range(0, location.Count); //随机 出生点的下标
             
                enemy.transform.position = location[index_location].position+new Vector3(Random.Range(-5,5),0,Random.Range(-5, 5)); //将怪兽移动到给定到的位置
                
                item.enemys.Add(enemy); //记录我生成的怪兽

                //  让 enemy 记住我属于那一波
                var enemy_scr = enemy.GetComponent<Enemy>();
                enemy_scr.wave = item;

                // 让 Enemy 速度发生变化，保证每一波敌人的速度不咋一样
                var agent = enemy.GetComponent<NavMeshAgent>();
                agent.speed *= item.factor;

                enemy.SetActive(true);

                yield return delay;
            }

            if (item.enable)
            {
                var wait = new WaitUntil(() => item.enemys.Count == 0);
                yield return wait;

                var wait_delay = new WaitForSeconds(item.delay);
                yield return wait_delay;
            }
        }
        OnAllWaveFinished.Invoke();
    }

    /// <summary>
    /// 开始生成敌人
    /// </summary>
    public void StartCreateEnemy() 
    {
        if (enemyCreateAction==null)
        {
            enemyCreateAction = StartCoroutine(CreateEnemy());
        }
    }

    /// <summary>
    /// 停止生成敌人
    /// </summary>
    public void StopCreateEnemy() 
    {
        if (null!=enemyCreateAction)
        {
            StopCoroutine(enemyCreateAction);
            enemyCreateAction = null;
        }
        foreach (var item in waves)
        {
            foreach ( var o in item.enemys)
            {
                DestroyImmediate(o);
            }
        }
    }

    [Serializable]
    public class WaveEvent : UnityEvent { }


    [System.Serializable]
    public class TransformList : ReorderableArray<Transform> { }

    [Serializable]
    public class WaveArray : ReorderableArray<Wave> { }


    [Serializable]
    public class Wave
    {
        [Header("波次名称")]
        public string name;
        [Header("波次是否生效")]
        public bool enable;
        [Header("怪兽的预制体")]
        public GameObject[] prefabs;
        [Header("生成的怪兽数量")]
        public int num;
        [Header("怪兽生成的间隔")]
        public float interval;
        [Header("下一波次的间隔，此时间段可用于做提示")]
        public float delay;
        [Header("速度因子（控制了速度的不同）"), Range(0.5f, 2f)]
        public float factor;

        [HideInInspector]
        public List<GameObject> enemys;
    }
}
