using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    private void Start() => FillData();
    public GameObject prefab;
    public Transform array_root;

    private void FillData()
    {
        ScoreManager.Instance.Load();

        // 本地函数，target ：目标 。 score ：分数数据
        void FillCell(Transform target ,ScoreManager.Score score)
        {
            //获取子节点下的所有 Text 组件
            var texts = target.GetComponentsInChildren<Text>();
            //获取名称为 name 的 Text 组件
            var name = texts.FirstOrDefault(v => v.name == "name");
            // 下同，略~
            var score_ = texts.FirstOrDefault(v => v.name == "score");
            // 下同，略~
            var date = texts.FirstOrDefault(v => v.name == "date");

            // 开始赋值
            name.text = score.name;
            score_.text = score.score.ToString();
            date.text = score.date;
        }

        foreach (var item in ScoreManager.Instance.rank.rank)
        {
            var go = Instantiate(prefab);
            go.transform.SetParent(array_root, false);
            FillCell(go.transform,item); //填充 细胞 数据
            go.SetActive(true);
        }
    }
}
