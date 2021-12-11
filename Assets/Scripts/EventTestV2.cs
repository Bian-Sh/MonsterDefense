using UnityEngine;
using Zinnia.Action;

public class EventTestV2 : MonoBehaviour
{
    public BooleanAction maxReached; //最大
    public BooleanAction midReached; //中间
    public BooleanAction minReached; //todo：最小稍后做
    void Start()
    {
        maxReached.Activated.AddListener(OnMaxReached);
        midReached.Activated.AddListener(OnMidReached);
        minReached.Activated.AddListener(OnMinReached);
    }

    private void OnMinReached(bool arg0)
    {
        Debug.Log($"OnMinReached : {arg0}");

    }

    private void OnMidReached(bool arg0)
    {
        Debug.Log($"OnMidReached : {arg0}");
    }

    private void OnMaxReached(bool arg0)
    {
        Debug.Log($"OnMaxReached : {arg0}");
    }

    private void OnDestroy()
    {
        maxReached.Activated.RemoveListener(OnMaxReached);
        midReached.Activated.RemoveListener(OnMidReached);
        minReached.Activated.RemoveListener(OnMinReached);
    }
}
