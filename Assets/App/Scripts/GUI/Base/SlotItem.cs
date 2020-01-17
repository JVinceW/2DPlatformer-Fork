using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour
{
    public Image icon;
    public Image BackGround;
    protected Equipment item;

    protected bool isShowToolTip = false;
    public virtual void Additem(Equipment newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        ChangeBackGround(item.quanlity);
    }
    public virtual void ClearSlot()
    {
        if (item != null)
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            ChangeBackGround(Quanlity.C);
        }
        isShowToolTip = false;
    }
    public virtual void UseItem()
    {
        isShowToolTip = false;
        if (item != null)
        {
            item.Use();
        }
    }
    void ChangeBackGround(Quanlity quanlity)
    {
        int index = (int)quanlity;
        BackGround.sprite = Inventory.instance.quanlity[index];
    }
    public void ShowToolTips()
    {
        if (item != null)
        {
            isShowToolTip = true;
        }
    }
    public void HideToolTips()
    {
        if (item != null)
        {
            isShowToolTip = false;
        }
    }

    private void OnGUI()
    {
        if (isShowToolTip && item != null)
        {
            // Set size box
            int BoxWidth = 32;
            Vector3 pos;
            pos.x = Input.mousePosition.x;
            pos.y = Screen.height - Input.mousePosition.y;

            //Config Content
            Color color = Inventory.instance.GetColorQuality(item.quanlity);
            string hex = ColorUtility.ToHtmlStringRGBA(color);
            string itemName = "\n<size=" + Setting.itemNameSize + "><color=#" + hex + "><b>" + item.Name + "</b></color></size>";
            string itemStat = "<size=" + Setting.itemStatSize + ">" +
                (item.Heath != 0 ? "\nHeath: " + item.Heath : "") +
                (item.Damage != 0 ? "\nAttack: " + item.Damage : "") +
                (item.Defense != 0 ? "\nArmor: " + item.Defense : "") +
                (item.Crit != 0 ? "\nCrit: " + item.Crit : "")+
                "</size>";
            string itemDescription = !string.IsNullOrEmpty(item.Description) ? "\n<size=" + Setting.itemDesSize + ">Detail: " + item.Description + "</size>":"";

            string tooltips = itemName + itemStat + itemDescription;
            GUIStyle style = GUI.skin.box;
            style.alignment = TextAnchor.MiddleCenter;


            //Vector2 posBackGround = new Vector2(posRect.x - 10, posRect.y);
            Vector2 sizeTooltip = style.CalcSize(new GUIContent(tooltips));

            Vector2 posRect = new Vector2(pos.x - sizeTooltip.x - BoxWidth / 2, pos.y);

            GUI.DrawTexture(new Rect(posRect, new Vector2(sizeTooltip.x + 20, sizeTooltip.y + 10)), (Texture2D)Resources.Load("GUI/Bags/Panel"), ScaleMode.StretchToFill);

            //Write Content
            GUI.Label(new Rect(new Vector2(posRect.x + BoxWidth / 3, posRect.y), sizeTooltip), tooltips);

        }
    }
}
