using TMPro;
using UnityEngine;

/// <summary>
/// 用于在手心展示用户昵称和当前的计分
/// 只需要将手心朝上即可展示
/// </summary>
public class HUDHolder : MonoBehaviour
{
    public Transform hand;
    public Vector3 offset;
    //定义旋转的角度的上限值
    public float targetAngle_up=300;
    //定义旋转的角度的下限值
    public  float targetAngle_dn=280;
    public GameObject hud;
    public TextMeshPro id;
    public TextMeshPro score;
    public Transform head;

    void Update()
    {
        var angle = hand.transform.localEulerAngles[2];
        //在指定的上下限夹角范围内我们显示 HUD
        if (angle >= targetAngle_dn&&angle<=targetAngle_up && !hud.activeInHierarchy)
        {
            hud.SetActive(true);
            id.text = $"昵称: {GameManager.Instance.player}";
            score.text = $"得分: {ScoreManager.Instance.score}";
            hud.transform.LookAt(head);
        }
        else if (angle < targetAngle_dn || angle > targetAngle_up)
        {
            hud.SetActive(false);
        }
        if (hud.activeInHierarchy)
        {
            score.text = $"得分: {ScoreManager.Instance.score}";
            hud.transform.position = hand.position + offset;
            hud.transform.LookAt(head);
        }
    }
}
