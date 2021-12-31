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
    public GameObject hud;
    public TextMeshPro id;
    public TextMeshPro score;
    public Transform head;

    void Update()
    {
        var angle = hand.transform.localEulerAngles[2];
        angle = angle >= 180 ? angle - 360 : angle;
        angle = Mathf.Abs(angle);
        if (angle >= 125 && !hud.activeInHierarchy)
        {
            hud.SetActive(true);
            id.text = $"昵称: {GameManager.Instance.player}";
            score.text = $"得分: {ScoreManager.Instance.score}";
            hud.transform.LookAt(head);
        }
        else if (angle<125)
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
