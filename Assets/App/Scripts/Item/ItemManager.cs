using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager
{
    #region Singleton
    static ItemManager m_instance;
    public static ItemManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new ItemManager();
            return m_instance;
        }
    }

    #endregion

    List<Equipment> equipment = new List<Equipment>();

    ItemManager()
    {
        LoadItem();
    }

    private void LoadItem()
    {
        equipment = new List<Equipment>();

        ArrayEquipment ar = new ArrayEquipment();
        ar.lstEquipment.Add(new Equipment());
        ar.lstEquipment.Add(new Equipment());
        ar.lstEquipment.Add(new Equipment());

        string EnemyDataPatsh = Setting.APPLICATION_PATH + "/" + Setting.ITEM_PATH;
        //string json = FileSystem.instance.Readfile(EnemyDataPath, false);

        string EnemyDataPath = Setting.DATA_PATH + Setting.ITEM_PATH;
        string json = FileSystem.instance.ReadFromResource(EnemyDataPath.Replace(".json", ""), false);
        //equipment = Jsons.LoadFromString<ArrayEquipment>(json).lstEquipment;
        string js = Jsons.CreateJsonFromObject<ArrayEquipment>(ar);
        FileSystem.instance.WriteToFile(EnemyDataPatsh, js, false);
        if (equipment.Count <= 0)
        {
            Logs.LogE("Load item fail");
        }
        else
        {
            Logs.LogE("Load item :" + equipment.Count);
        }
    }

    public Equipment GetItem(int ID)
    {
        if(equipment == null)
        {
            return null;
        }
        for(int i = 0; i < equipment.Count; i++)
        {
            if (equipment[i] == null)
                continue;
            if (equipment[i].ItemID == ID)
                return equipment[i];
        }
        return null;
    }

}

[SerializeField]
public class ArrayEquipment
{
    public List<Equipment> lstEquipment = new List<Equipment>();
}
