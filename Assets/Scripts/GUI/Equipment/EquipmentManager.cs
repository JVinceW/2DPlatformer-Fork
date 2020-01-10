using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region singleton
    public static EquipmentManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Logs.LogW("More than one instance of EquipmentManager");
            return;
        }
        instance = this;
    }
    #endregion

    public Equipment[] CurrentEquipment;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged OnEquipmentChangedCallBack;
    private void Start()
    {
        int Slot = System.Enum.GetNames(typeof(EquipmentType)).Length;
        CurrentEquipment = new Equipment[Slot];
    }
    public void Equip(Equipment newItem)
    {
        Equipment oldItem = null;

        int SlotIndex = (int)newItem.equipSlot;
        if(CurrentEquipment[SlotIndex] != null)
        {
            oldItem = CurrentEquipment[SlotIndex];
            Inventory.instance.Add(oldItem);
        }

        CurrentEquipment[SlotIndex] = newItem;

        if(OnEquipmentChangedCallBack != null)
        {
            OnEquipmentChangedCallBack.Invoke(newItem, oldItem);
        }

    }
    public void Unequip(Equipment equipment)
    {
        int SlotIndex = (int)equipment.equipSlot;
        if (CurrentEquipment[SlotIndex] != null)
        {
            Equipment oldItem = CurrentEquipment[SlotIndex];
            Inventory.instance.Add(oldItem);

            CurrentEquipment[SlotIndex] = null;

            // Equipment has been removed so we trigger the callback
            if (OnEquipmentChangedCallBack != null)
                OnEquipmentChangedCallBack.Invoke(null, oldItem);

        }
    }
}
