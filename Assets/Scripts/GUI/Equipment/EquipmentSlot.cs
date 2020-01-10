using UnityEngine;
using UnityEngine.UI;


public class EquipmentSlot : SlotItem
{
    public override void ClearSlot()
    {
        if(item != null)
        {
            EquipmentManager.instance.Unequip(item);
        }
        base.ClearSlot();
    }
    public override void UseItem()
    {
        isShowToolTip = false;
        if (item != null)
        {
            Inventory.instance.Remove(item);
        }
    }
}
