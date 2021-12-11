using UnityEngine;
public class TouchPadClickEventReceiver : MonoBehaviour
{
    [SerializeField]
    TouchPadClickEventDispatcher dispatcher;
    private void Start() => dispatcher.OnTouchPadClicked.AddListener(OnTouchPadClicked);
    private void OnTouchPadClicked(TouchPadDirection arg0)
    {
        switch (arg0)
        {
            case TouchPadDirection.Up:
                Debug.Log($"{nameof(TouchPadClickEventReceiver)}:  上 ");
                break;
            case TouchPadDirection.Bottom:
                Debug.Log($"{nameof(TouchPadClickEventReceiver)}:  下 ");
                break;
            case TouchPadDirection.Right:
                Debug.Log($"{nameof(TouchPadClickEventReceiver)}:  右 ");
                break;
            case TouchPadDirection.Left:
                Debug.Log($"{nameof(TouchPadClickEventReceiver)}:  左 ");
                break;
            default:
                break;
        }
    }
}
