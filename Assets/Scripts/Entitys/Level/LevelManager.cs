using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    private int m_iLevel;
    private int m_iCurrentExp;
    private int m_iExpForLevelUp;
    public LevelManager()
    {
        m_iLevel = 0;
        m_iCurrentExp = 0;
    }
    //GetFunction
    public int GetLevel()
    {
        return m_iLevel > 0 ? m_iLevel : 0;
    }
    public int GetExp()
    {
        return m_iCurrentExp > 0 ? m_iCurrentExp : 0;
    }
    //Set function
    /// <summary>
    /// Add exp for object
    /// </summary>
    /// <param name="exp">Exp will be add</param>
    public void AddExp(int exp)
    {
        //if exp or current exp < 0, it will not do anything
        if (exp <= 0)
            return;
        if (m_iCurrentExp < 0)
            return;
        m_iCurrentExp += exp;
        if(m_iCurrentExp >= m_iExpForLevelUp)
        {
            m_iLevel++;
            m_iCurrentExp -= m_iExpForLevelUp;
            GetExpForLevelUp();
        }
    }

    private void GetExpForLevelUp()
    {
        m_iExpForLevelUp = Data.instance.GetNexExpForLevelUp(m_iLevel);
    }

}
