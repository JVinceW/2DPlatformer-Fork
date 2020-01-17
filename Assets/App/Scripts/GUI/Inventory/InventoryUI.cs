using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Singleton
    public InventoryUI instance;
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion

    public GameObject invetoryUI;
    public Transform itemsParent;
    Inventory inventory;

    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(PlayerManager.instance.kInventory))
        {
            ShowUI();
        }
    }
    void UpdateUI()
    {
        Logs.LogW("Update Inventory UI");
        for(int i=0;i<slots.Length;i++)
        {
            if(i < inventory.Lstitems.Count)
            {
                slots[i].Additem(inventory.Lstitems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    public void ShowUI()
    {
        invetoryUI.SetActive(!invetoryUI.activeSelf);
    }
}
