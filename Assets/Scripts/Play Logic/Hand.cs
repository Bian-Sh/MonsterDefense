using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    /// <summary>关心的动画机 </summary>
    public Animator animator;
    private bool isGrabed; //是否已经抓住

    public void 抓握()
    {
        animator.SetBool("抓握", true);
    }

    public void 丢枪()
    {
        animator.SetBool("抓握", false);
    }

    public void 开枪()
    {
        animator.SetTrigger("开火");
    }

    private void Reset()
    {
        animator = GetComponent<Animator>();
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
