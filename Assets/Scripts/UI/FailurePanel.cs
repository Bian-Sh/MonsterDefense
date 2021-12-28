using UnityEngine;
using UnityEngine.UI;

public class FailurePanel: MonoBehaviour
{
    [SerializeField, Header("重新开始")] Button button_scene;
    [SerializeField, Header("退出游戏")] Button button_exit;

    void Start()
    {
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
}
