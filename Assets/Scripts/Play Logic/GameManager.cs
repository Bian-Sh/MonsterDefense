using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 游戏管理器
/// 1. 游戏开始结束
/// 2. 玩家（昵称）管理
/// 3. 场景转换
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    WaveManager waveManager; //敌人波次管理器
    [Space(10)]
    /// <summary>昵称</summary>
    public List<string> nickName;
    public TouchPadClickEventDispatcher touchPadClickEventDispatcher;

    public GameObject panel;


    /// <summary>
    /// 当前的玩家
    /// </summary>
    [Header("玩家的昵称：")]
    public string player;

    #region 单例
    public static GameManager Instance { get; private set; }
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
        // 监听场景加载完成的事件
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    #endregion



    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log($"{nameof(GameManager)}: 场景加载完毕，当前场景为： {arg0.name}");
        if (arg0.name == "MainScene")
        {
            touchPadClickEventDispatcher = FindObjectOfType<TouchPadClickEventDispatcher>();
            touchPadClickEventDispatcher.OnTouchPadClicked.AddListener(OnTouchPadClicked);
            panel = GameObject.Find("Canvas/RankPanel");
        }
    }

    private void OnTouchPadClicked(TouchPadDirection arg0)
    {
        Debug.Log($"{nameof(GameManager)}: 你按下了 {arg0 } 键！");
    }

    private void Start()
    {
        // 拼接昵称文件的路径
        var path = Path.Combine(Application.streamingAssetsPath, "Data/昵称.txt");
        //读取 昵称数据
        if (File.Exists(path))
        {
            var info = File.ReadAllLines(path);
            nickName = new List<string>(info);
        }
        waveManager.OnAllWaveFinished.AddListener(OnAllWaveFinished);
    }

    private void OnAllWaveFinished()
    {
        ScoreManager.Instance.Save();
        panel.SetActive(true);
    }

    /// <summary>
    /// 获取随机的昵称
    /// </summary>
    /// <returns>昵称</returns>
    public string GetRandomName()
    {
        var index = Random.Range(0, nickName.Count);
        player = nickName[index];
        return player;
    }




    public void GameOver()
    {
        waveManager.StopCreateEnemy();
        Time.timeScale = 0;
    }





    private void Reset()
    {
#if UNITY_EDITOR
        waveManager = GetComponentInChildren<WaveManager>();
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
