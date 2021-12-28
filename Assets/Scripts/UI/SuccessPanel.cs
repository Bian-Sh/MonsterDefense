using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuccessPanel : MonoBehaviour
{

    [SerializeField,Header("排行榜")] Button button_rank;
    [SerializeField, Header("重新开始")] Button button_scene;
    [SerializeField, Header("退出游戏")] Button button_exit;
    [SerializeField,Header("排行榜")] GameObject panel ;

    void Start()
    {
        button_rank.onClick.AddListener(OnRankButtonClicked);
        button_scene.onClick.AddListener(OnSceneChangeRequested);
        button_exit.onClick.AddListener(OnExitGameRequest);
    }

    /// <summary>退出游戏</summary>
    private void OnExitGameRequest()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
# endif
    }
    
    /// <summary> 请求重新开始游戏</summary>
    private void OnSceneChangeRequested()
    {
        GameManager.Instance.ReloadGame();
    }


    /// <summary>
    /// 用户请求刷新昵称
    /// </summary>
    private void OnRankButtonClicked()
    {
        panel.SetActive(true);
    }

}
