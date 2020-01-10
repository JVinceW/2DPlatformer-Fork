using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{

    #region Singleton
    public EquipmentUI instance;
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion

    public GameObject equimentUI;
    public Transform parentEquipment;
    EquipmentSlot[] LstEquipmentSlot;

    private void Start()
    {
        EquipmentManager.instance.OnEquipmentChangedCallBack += UpdateUI;
        LstEquipmentSlot = parentEquipment.GetComponentsInChildren<EquipmentSlot>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(PlayerManager.instance.kInfomation))
        {
            ShowUI();
        }
    }
    void UpdateUI(Equipment newitem, Equipment olditem)
    {
        for (int i = 0; i < LstEquipmentSlot.Length; i++)
        {
            if (i < EquipmentManager.instance.CurrentEquipment.Length)
            {
                if(EquipmentManager.instance.CurrentEquipment[i] != null)
                {
                    LstEquipmentSlot[i].Additem(EquipmentManager.instance.CurrentEquipment[i]);
                }
            }
            else
            {
                LstEquipmentSlot[i].ClearSlot();
            }
        }
    }
    public void ShowUI()
    {
        equimentUI.SetActive(!equimentUI.activeSelf);
    }
}
