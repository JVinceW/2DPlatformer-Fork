using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : SlotItem
{
    public Button removeButton;

    public override void Additem(Equipment newItem)
    {
        base.Additem(newItem);
        removeButton.interactable = true;
    }
    public override void ClearSlot()
    {
        base.ClearSlot();
        removeButton.interactable = false;
    }
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }
}
