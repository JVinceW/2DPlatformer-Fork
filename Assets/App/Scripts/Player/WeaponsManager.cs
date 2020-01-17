using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager
{
    public static WeaponsManager m_instance;
    public bool visible = false;
    private List<GameObject> List_Weapons;
    private int CurrentWeapon = 0;
    // Start is called before the first frame update
    public static WeaponsManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new WeaponsManager();
            return m_instance;
        }
        set
        {
            m_instance = value;
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        if(weapon == null)
        {
            Logs.LogE("No weapon to add");
            return;
        }
        if(List_Weapons == null)
        {
            List_Weapons = new List<GameObject>();
        }
        List_Weapons.Add(weapon);
    }

    public GameObject GetWeapon()
    {
        if(List_Weapons.Count <=0)
        {
            Logs.LogE("Dont have any weapons, Lenght = 0");
            return null;
        }
        return List_Weapons[CurrentWeapon];
    }

    public string GetWeaponName()
    {
        return List_Weapons[CurrentWeapon].name;
    }

    public int CountWeapons()
    {
        if (List_Weapons == null)
        {
            return -1;
        }
        return List_Weapons.Count;
    }

    public void Next()
    {
        Logs.LogD("Next weapon");
        CurrentWeapon++;
        if (CurrentWeapon >= List_Weapons.Count)
        {
            CurrentWeapon = 0;
        }
    }
    public void Previous()
    {
        Logs.LogD("Pre weapon");
        CurrentWeapon--;
        if (CurrentWeapon < 0)
        {
            CurrentWeapon = List_Weapons.Count - 1;
        }
    }

}
