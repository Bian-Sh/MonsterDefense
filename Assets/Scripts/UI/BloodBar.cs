using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
public class BloodBar : MonoBehaviour
{
    public Image image; //血条
    [Header("血条跟随目标")]
    public Transform target; //目标
    [Header("偏移量")]
    public Vector3 offset; //偏移量

    void Start()
    {
        var Enemy = target.GetComponentInChildren<IBloodPercentage>();
        Enemy.OnBloodChanged.AddListener(OnBloodChanged);
    }

    private void OnBloodChanged(float arg0)
    {
        image.DOFillAmount(arg0,0.5F);
    }

    void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
        }
        else
        {
            CaculatePosition();
        }
    }

    private void OnValidate()
    {
        CaculatePosition();
    }


    public void CaculatePosition()
    {
        transform?.LookAt(Camera.main?.transform);
    }

}

/// <summary>
/// 血量
/// </summary>
public interface IBloodPercentage 
{
    BloodEvent OnBloodChanged { get; set; }
}

[Serializable]
public class BloodEvent : UnityEngine.Events.UnityEvent<float> { };