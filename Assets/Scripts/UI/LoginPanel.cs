using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{

    [SerializeField] InputField inputField;
    [SerializeField] Button button;
    [SerializeField] Button button_scene;
    [SerializeField] Button button_exit;
    Text button_scene_text;
    [Header("游戏场景：")]
    public string scene = "MainScene";

    void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
        button_scene.onClick.AddListener(OnSceneChangeRequested);
        button_exit.onClick.AddListener(OnExitGameRequest);

        inputField.onEndEdit.AddListener(OnInputFieldFinishEdit);
        inputField.onValueChanged.AddListener(OnUserChangedInputField);
        button_scene_text = button_scene.GetComponentInChildren<Text>();

        inputField.text = GameManager.Instance.GetRandomName();
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

    private void OnUserChangedInputField(string arg0)
    {
        if (string.IsNullOrEmpty(arg0))
        {
            button_scene.interactable = false;
            button_scene_text .text= $"请先指定昵称";
        }
        else
        {
            button_scene.interactable = true;
            button_scene_text.text = $"开始游戏";
        }
    }

    /// <summary> 请求转换场景 </summary>
    private void OnSceneChangeRequested()
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// 完成昵称的输入
    /// </summary>
    /// <param name="arg0"></param>
    private void OnInputFieldFinishEdit(string arg0)
    {
        Debug.Log($"{nameof(LoginPanel)}:用户输入了 = {arg0}");
        GameManager.Instance.player = arg0;
    }

    /// <summary>
    /// 用户请求刷新昵称
    /// </summary>
    private void OnButtonClicked()
    {
        var name = GameManager.Instance.GetRandomName();
        inputField.text = name;
    }

}
