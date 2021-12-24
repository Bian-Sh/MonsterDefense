using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 积分管理器
///  1. 加载积分
///  2. 记录积分
///  3. 保存积分
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [Header("排行榜容量：")]
    public int capacity;
    public ScoreData rank; //排行榜信息

    /// <summary>
    /// 玩家的得分
    /// </summary>
    [Header("玩家当前得分：")]
    public int score;
    [SerializeField, Header("取值的KEY")]
    private string key = "ScoreManager_Info_9527";
    private Score score_data;
    #region 单例
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            DestroyImmediate(gameObject);
        }
    }
    #endregion

    private void Start() => Load();


    /// <summary>
    /// 加载积分排行
    /// </summary>
    public void Load()
    {
        // PlayerPrefs //存储少量数据到本地的 API 
        // Json 的使用
        var json = PlayerPrefs.GetString(key, JsonUtility.ToJson(rank));
        rank = JsonUtility.FromJson<ScoreData>(json);
        Debug.Log($"{nameof(ScoreManager)}: 用户加载了数据！请查询 ScoreManager 面板");
    }

    /// <summary>
    /// 保存积分
    /// </summary>
    public void Save()
    {
        // 构建一个分数的数据实例
        if (score_data == null)
        {
            score_data = new Score
            {
                name = GameManager.Instance.player,
                score = score,
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
        else
        {
            score_data.score = score;
            score_data.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // 在解决溢出的情况下填充数据
        while (rank.rank.Count >= capacity)
        {
            rank.rank.RemoveAt(0);
        }

        // 如果排行榜中已经存在，则无须再次插入该笔数据
        if (!rank.rank.Contains(score_data))
        {
            rank.rank.Add(score_data);
        }

        // 对分数进行排序
        rank.rank.Sort((a,b)=>b.score-a.score);

        //  反序列化数据到 json
        var json = JsonUtility.ToJson(rank);

        // 存储反序列化好的数据到 PlayerPrefs 
        PlayerPrefs.SetString(key, json);
        Debug.Log($"{nameof(ScoreManager)}: 用户存储了数据 \n {json}");

        // PlayerPrefs 存到了哪儿？
        // 注册表 
        // 1 编辑器下：计算机\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\MonsterDefense
        // 2  运行时（打包后）：计算机\HKEY_CURRENT_USER\SOFTWARE\DefaultCompany\MonsterDefense

    }


    /// <summary>
    /// 排行榜信息
    /// </summary>
    [Serializable]
    public class ScoreData
    {
        [Header("排行榜")]
        public List<Score> rank;
    }

    /// <summary>
    /// 分数项数据结构
    /// </summary>
    [Serializable]
    public class Score
    {
        /// <summary>   玩家  </summary>
        public string name;
        /// <summary>   积分 </summary>
        public int score;
        /// <summary>   达成时间 </summary>
        public string date;
    }
}
