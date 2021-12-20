using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour, IBloodPercentage
{
    public BloodEvent OnBloodChanged { get; set; } = new BloodEvent();
    public TowerEvent OnTowerDestroy = new TowerEvent();
    public float HP;
    private float HP_Cached;
    private void Start()
    {
        HP_Cached = HP;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (HP > 0)
        {
            var at = other.GetComponentInChildren<IAttack>();
            var go = at as Object;
            if (go.name.Contains("Projectile"))
            {
                var value = Random.Range(0, 10) >= 5;
                HP -= value ? 0f : at.AT;
                Debug.Log($"{nameof(Tower)}:  {(value ? "免疫此次攻击" : $"友军伤害 = {at.AT}")}");
            }
            else
            {
                HP -= at.AT;
            }

            HP = HP < 0 ? 0 : HP;

            OnBloodChanged.Invoke(HP / HP_Cached);

            if (HP == 0)
            {
                OnTowerDestroy.Invoke();
            }
        }
    }

    [Serializable]
    public class TowerEvent : UnityEngine.Events.UnityEvent { }
}
