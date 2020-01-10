using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int ItemID = 0;
    public string Name = "New item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public Quanlity quanlity = Quanlity.C;
    public string Description = "";
    public virtual void Use()
    {
        Logs.LogD("User item " + Name);
    }
}
public enum Quanlity {C, B, A, S, L, SL}
