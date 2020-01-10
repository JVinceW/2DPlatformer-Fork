using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager
{
    #region singleton
    static EnemyManager m_instance;
    public static EnemyManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new EnemyManager();
            return m_instance;
        }
    }
    #endregion
    public List<EnemyStat> lstEnemy;
    public void LoadResource()
    {
        lstEnemy = new List<EnemyStat>();
        //string EnemyDataPath = Setting.APPLICATION_PATH + "/" + Setting.ENEMY_PATH;
        //string json = FileSystem.instance.Readfile(EnemyDataPath, false);

        string EnemyDataPath = Setting.DATA_PATH + Setting.ENEMY_PATH;
        string json = FileSystem.instance.ReadFromResource(EnemyDataPath.Replace(".json",""), false);
        lstEnemy = Jsons.LoadFromString<ArrayEnemy>(json).lstEnemy;
        if(lstEnemy.Count <=0 )
        {
            Logs.LogE("Load enemy fail");
        }
    }
    public void Save()
    {
        //string json = Jsons.CreateJsonFromObject<ArrayEnemy>(lstEnemy);
        //FileSystem.instance.WriteToFile(Setting.APPLICATION_PATH + "/" + Setting.ENEMY_PATH, json, false);
    }

    public EnemyStat GetEnemyAttribute(int ID)
    {
        if(lstEnemy == null)
        {
            LoadResource();
        }
        var reuslt = lstEnemy.Where(i => i.ID == ID).SingleOrDefault();
        if(reuslt == null)
        {
            Logs.LogE("Error on load Enemy ID: " + ID);
        }
        return reuslt;
    }

}
[SerializeField]
public class ArrayEnemy
{
    public List<EnemyStat> lstEnemy;
}
