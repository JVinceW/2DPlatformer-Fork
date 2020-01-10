using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Logs.LogW("More than one instance of Inventory");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public List<Equipment> Lstitems = new List<Equipment>();
    public int m_Space = 9 * 8;
    public Sprite[] quanlity = new Sprite[6];
    public Vector2 Pixelcolor = new Vector2(10,10);
    public void Add(Equipment item)
    {
        if (!item.isDefaultItem)
        {
            if(Lstitems.Count >= m_Space)
            {
                Logs.LogE("Not enough room.");
                return;
            }
            Lstitems.Add(item);
            if(onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
        }
    }
    public void Remove(Equipment item)
    {
        Lstitems.Remove(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
    public Color GetColorQuality(Quanlity color)
    {
        if(quanlity != null)
        {
            return quanlity[(int)color].texture.GetPixel((int)Pixelcolor.x, (int)Pixelcolor.y);
            //switch(color)
            //{
            //    case Quanlity.C: return Color.white;
            //    case Quanlity.B: return Color.green;
            //    case Quanlity.A: return Color.blue;
            //    case Quanlity.S: return Color.yellow;
            //    case Quanlity.L: return new Color(153, 50, 204);
            //    case Quanlity.SL: return Color.red;
            //}
        }
        return Color.white;
    }


}
