using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    static Data s_instance;
    public static Data instance
    {
        get
        {
            if (s_instance == null)
                s_instance = new Data();
            return s_instance;
        }
        set
        {
            s_instance = value;
        }
    }

    public Data()
    {
        //Load Data from DataBase
    }
    //Get function
    public int GetNexExpForLevelUp(int currenLevel)
    {
        return 99999999;
    }

}
