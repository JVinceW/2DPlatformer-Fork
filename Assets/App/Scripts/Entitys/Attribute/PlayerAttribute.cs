using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerAttribute : IAttributes
{
    #region Singleton
    public static PlayerAttribute instance;
    public override void Awake()
    {
        if(instance != null)
        {
            Logs.LogW("more than instance PlayerAttribute");
            return;
        }
        base.Awake();
        instance = this;
    }
    #endregion

    public delegate void OnAttributeChange();
    public OnAttributeChange OnAttributeChangeCallBack;

    public int Exp;
    [SerializeField]
    public override void Start()
    {
        LoadProfile();
        EquipmentManager.instance.OnEquipmentChangedCallBack += OnEquipMentChange;
    }
    void LoadProfile()
    {
        //Load profile
        try
        {
            string profile = Profile.instance.GetProfile(false);
            if (!string.IsNullOrEmpty(profile))
            {
                Operator(Jsons.LoadFromString<PlayerStat>(profile));
                transform.parent.transform.position = this.Position;
                transform.parent.transform.rotation = Quaternion.Euler(Rotation);
                transform.parent.transform.localScale = this.Scale;
            }
        }
        catch(Exception ex)
        {
            Logs.LogE("Can't load profile : " + ex.ToString());
        }
    }
    void Operator(PlayerStat playerAttribute)
    {
        this.ID = playerAttribute.ID;
        this.Name = playerAttribute.Name;
        this.Level = playerAttribute.Level;
        this.Heath = playerAttribute.Heath;
        this.CurrentHeath = playerAttribute.CurrentHeath;
        this.Damage = playerAttribute.Damage;
        this.Defense = playerAttribute.Defense;
        this.Crit = playerAttribute.Crit;
        this.Position = playerAttribute.Position;
        this.Rotation = playerAttribute.Rotation;
        this.Scale = playerAttribute.Scale;
    }
    void OnEquipMentChange(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
        {
            this.Heath.AddModifier(newItem.Heath);
            this.CurrentHeath += newItem.Heath;
            this.Damage.AddModifier(newItem.Damage);
            this.Defense.AddModifier(newItem.Defense);
            this.Crit.AddModifier(newItem.Crit);
        }
        if(oldItem != null)
        {
            this.Heath.RemoveModifier(oldItem.Heath);
            this.CurrentHeath -= oldItem.Heath;
            this.Damage.RemoveModifier(oldItem.Damage);
            this.Defense.RemoveModifier(oldItem.Defense);
            this.Crit.RemoveModifier(oldItem.Crit);
        }
        if(OnAttributeChangeCallBack != null)
        {
            OnAttributeChangeCallBack.Invoke();
        }
    }
    void SaveProfile()
    {
        this.Scale = transform.parent.transform.localScale;
        this.Rotation = transform.parent.transform.localRotation.eulerAngles;
        this.Position = transform.parent.transform.localPosition;
        string jsonString = Jsons.CreateJsonFromObject<PlayerAttribute>(this);
        Profile.instance.SaveProfile(jsonString, false);
    }

    public override void TakeDamage(IAttributes attributes)
    {
        if(attributes != null)
        {
            base.TakeDamage(attributes);
        }
        if (OnAttributeChangeCallBack != null)
        {
            OnAttributeChangeCallBack.Invoke();
        }
    }
    public override void Heal(float amount)
    {
        base.Heal(amount);
        if (OnAttributeChangeCallBack != null)
        {
            OnAttributeChangeCallBack.Invoke();
        }
    }
}
