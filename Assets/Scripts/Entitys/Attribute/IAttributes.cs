using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttributes:MonoBehaviour
{
    public int ID;
    [HideInInspector]
    public string Name;
    public int Level;

    public Stats Heath;
    //[HideInInspector]
    public float CurrentHeath;

    public Stats Damage;
    public Stats Defense;
    public Stats Crit;

    [HideInInspector]
    public Vector3 Position;
    [HideInInspector]
    public Vector3 Rotation;
    [HideInInspector]
    public Vector3 Scale;

    public delegate void OnDie();
    public event OnDie OnDieTrigger;
    public virtual void Awake()
    {
        if(Heath.GetValue() >= 0)
        {
            CurrentHeath = Heath.GetValue();
        }
    }
    public virtual void Start()
    {

    }
    public virtual void TakeDamage(IAttributes attributes)
    {
        if (attributes == null)
            return;
        float Damage = IMath.instance.CaculatorDamage(attributes.Damage.GetValue(), Defense.GetValue(), attributes.Crit.GetValue());
        CurrentHeath -= Mathf.Clamp(Damage, 0, float.MaxValue);
        Logs.LogD("Current Heath" + CurrentHeath);
        if(CurrentHeath <= 0)
        {
            Die(attributes);
        }
    }

    public float GetHealthPercent()
    {
        return Mathf.Clamp01(CurrentHeath / Heath.GetValue());
    }
    public float GetCurrentHeath()
    {
        return CurrentHeath;
    }
    public virtual void Heal(float amount)
    {
        CurrentHeath += amount;
        CurrentHeath = Mathf.Clamp(CurrentHeath,0, Heath.GetValue());
    }
    public virtual void Die(IAttributes attributes)
    {
        if(attributes != null)
        {
            Logs.LogD(this.name + " killed  by " + attributes.Name + " at " + DateTime.Now.ToString());
        }
        if(OnDieTrigger != null)
        {
            OnDieTrigger.Invoke();
        }
    }
}
