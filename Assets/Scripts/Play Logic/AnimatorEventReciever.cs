using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画机事件接受器
/// </summary>
public class AnimatorEventReciever : MonoBehaviour
{
    public void DeathCallback() 
    {
        Debug.Log($"{nameof(AnimatorEventReciever)}:  额死了~");
        Destroy(transform.parent.gameObject);
    }
}
