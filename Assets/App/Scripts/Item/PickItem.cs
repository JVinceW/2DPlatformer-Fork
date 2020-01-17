using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    public Equipment item;
    public void SetItem(Equipment items,Sprite sprite)
    {
        this.item = items;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == PlayerManager.instance.GetPlayerTag())
        {
            if(item != null)
            {
                Destroy(this.gameObject);
                Inventory.instance.Add(item);
                item = null;
            }
        }
    }
}
