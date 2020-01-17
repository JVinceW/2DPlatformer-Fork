using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IMath
{
    static IMath m_instance;
    public static IMath instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new IMath();
            return m_instance;
        }
    }
    public float CaculatorDamage(float Damage, float Defense, float Crits=1)
    {
        //Caculator Damage
        /*
        (((C*2 / D)*X)*Z)/ 255

        C: Damage
        D: Defense
        X : Crits
        Z : random number from 200 to 255
        */
        Defense = Defense == 0 ? 1 : Defense;
        Crits = Crits <= 0 ? 1 : Crits;
        Crits = Crits / 100f;

        var Z = UnityEngine.Random.Range(200, 255);
        var TotalDamage = (((Damage * 2 / Defense) * (1 + Crits)) * Z) / 255;
        TotalDamage = (float)Math.Round(TotalDamage, 1);
        return TotalDamage;
    }

    public int GetInt(float value)
    {
        return (int)value;
    }

}
