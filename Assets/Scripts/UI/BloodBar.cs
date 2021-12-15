using UnityEngine;
using UnityEngine.UI;
using System;

public class BloodBar : MonoBehaviour
{
    public Image image; //血条
    [Header("血条跟随目标")]
    public Transform target; //目标
    [Header("偏移量")]
    public Vector3 offset; //偏移量

    void Start()
    {
        var Enemy = target.GetComponentInChildren<Enemy>();
        Enemy.OnEnemyHited.AddListener(OnEnemyHitted);
    }

    private void OnEnemyHitted(float arg0)
    {
        image.fillAmount = arg0;
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
        transform.LookAt(Camera.main.transform);
    }

}
