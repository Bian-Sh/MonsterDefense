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
    public WaveManager waveManager; //敌人波次管理器
    [Space(10)]
    /// <summary>昵称</summary>
    public List<string> nickName;

    /// <summary>
    /// 当前的玩家
    /// </summary>
    [Header("玩家的昵称：")]
    public string player;
    public static bool isPlaying;

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
    }

    internal void ReloadGame()
    {
        //重置波次管理器
        waveManager.StopCreateEnemy();
        //重置分数管理器
        ScoreManager.Instance.ResetData();
        //重新加载场景
        var scene = SceneManager.GetActiveScene();
        Destroy(gameObject);
        SceneManager.LoadScene(scene.name);
    }
    #endregion


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

    /// <summary>
    /// 游戏开始
    /// </summary>
    internal void StartGame()
    {
        isPlaying = true;
        waveManager.StartCreateEnemy();
    }
    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
        isPlaying = false;
        waveManager.StopCreateEnemy(false);
    }


    private void Reset()
    {
#if UNITY_EDITOR
        waveManager = GetComponentInChildren<WaveManager>();
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
