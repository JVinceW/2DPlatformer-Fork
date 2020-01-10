using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType equipSlot;
    public int Heath;
    public int Damage;
    public int Defense;
    public int Crit;
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        Inventory.instance.Remove(this);
    }
}
public enum EquipmentType { Head, Chest, Ring, Legs, Hands, Weapon , Orb}