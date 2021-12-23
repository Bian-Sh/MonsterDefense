using System;
using System.Linq;
using Tilia.Input.UnityInputManager;
using UnityEngine;
using UnityEngine.Events;
public enum TouchPadDirection
{
    Up = 0,
    Bottom = 2,
    Left = 1,
    Right = 3,
}
/// <summary>
/// 脚本用于判断你按下HTC vive TouchPad（上下左右）的哪一个区域了
/// </summary>
public class TouchPadClickEventDispatcher : MonoBehaviour
{
    [Header("Touch Pad 点击事件")]
    public TouchPadClickEvent OnTouchPadClicked = new TouchPadClickEvent();

    private void Start() => buttonAction.Activated.AddListener(OnButtonActionActived);
    private void OnButtonActionActived(bool arg0)
    {
        var angle = Vector2.SignedAngle(new Vector2(1, 1), new Vector2(hr.Value, vt.Value));
        //带符号角度转换为 0°~360°
        angle += angle < 0 ? 360 : 0;
        //每 90° 的刻度划分一个方向，逆时针进行
        var dir = (TouchPadDirection)Mathf.FloorToInt(angle / 90);
        OnTouchPadClicked.Invoke(dir);
    }

    private void Reset()
    {
        var bts = GetComponentsInChildren<UnityInputManagerButtonAction>();
        var _1ds = GetComponentsInChildren<UnityInputManagerAxis1DAction>();
        buttonAction = bts.FirstOrDefault(v => v.name.Contains("Trackpad_Press"));
        hr = _1ds.FirstOrDefault(v => v.name.Contains("Trackpad_HorizontalAxis"));
        vt = _1ds.FirstOrDefault(v => v.name.Contains("Trackpad_VerticalAxis"));
        if (!buttonAction || !hr || !vt)
        {
            Debug.LogError("TouchPadClickEventDispatcher  挂载失败 \n 请挂载在 Input.UnityInputManager.OpenVR.RightController 或者 Input.UnityInputManager.OpenVR.LeftController");
            DestroyImmediate(this);
        }
        else
        {
            //由于此方法打包时会被剥离，所以，我选择不使用宏包裹，声明：笔者对本行备注不负责
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
    [SerializeField, HideInInspector]
    UnityInputManagerButtonAction buttonAction;
    [SerializeField, HideInInspector]
    UnityInputManagerAxis1DAction hr;
    [SerializeField, HideInInspector]
    UnityInputManagerAxis1DAction vt;
    [Serializable]
    public class TouchPadClickEvent : UnityEvent<TouchPadDirection> { }
}
