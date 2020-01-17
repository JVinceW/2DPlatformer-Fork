using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : IAttributes
{
    public Transform HeathBar;
    public bool IsRetransform;
    public override void Start()
    {
        //Load profile
        LoadAttribute(ID);
    }
    private void OnGUI()
    {
        if (DebugManager.instance.isDebug())
        {
            Vector2 size = new Vector2(120, 20);
            Vector3 pos = Camera.main.WorldToScreenPoint(HeathBar.position);
            pos.y = Screen.height - pos.y;
            GUI.Box(new Rect(pos.x - size.x / 2, pos.y, size.x, size.y), CurrentHeath + "/" + Heath.GetValue());
            GUI.enabled = true;

        }
    }
    void LoadAttribute(int ID)
    {
        try
        {
            EnemyStat stat = new EnemyStat();
            stat = EnemyManager.instance.GetEnemyAttribute(ID);
            if (stat != null)
            {
                Name = stat.Name;

                Heath.Value = stat.Heath.Value;
                Heath.AddModifier(stat.Heath.Value * stat.Level);
                CurrentHeath = Heath.GetValue();

                Damage.Value = stat.Damage.Value;
                Damage.AddModifier(stat.Damage.Value * stat.Level);

                Defense.Value = stat.Defense.Value;
                Defense.AddModifier(stat.Defense.Value * stat.Level);

                Crit.Value = stat.Crit.Value;

                if(IsRetransform)
                {
                    transform.parent.transform.position = stat.Position;
                    transform.parent.transform.rotation = Quaternion.Euler(stat.Rotation);
                    transform.parent.transform.localScale = stat.Scale;
                }
            }
        }
        catch (Exception ex)
        {
            Logs.LogE("Can't load emeny ID "+ ID +" : " + ex.ToString());
        }
    }
}
