using UnityEngine;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(BloodBar))]
class BloodBarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("定位到角色"))
        {
            var bar = target as BloodBar;
            bar.CaculatePosition();
        }
    }
}
#endif

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
        image.transform.position = target.position + offset;
    }

}
