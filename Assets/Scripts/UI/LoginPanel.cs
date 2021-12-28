using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Button button;
    [SerializeField] Button button_start;
    [SerializeField] Button button_exit;

    void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
        button_start.onClick.AddListener(OnStartGameRequested);
        button_exit.onClick.AddListener(OnExitGameRequest);

        inputField.onEndEdit.AddListener(OnInputFieldFinishEdit);
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

    /// <summary> 请求开始游戏 </summary>
    private void OnStartGameRequested()
    {
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
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
