using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jsons
{
    public static T LoadFromString<T>(string jsonString)
    {
        return JsonUtility.FromJson<T>(jsonString);
    }
    public static string CreateJsonFromObject<T>(T objects)
    {
        return JsonUtility.ToJson(objects);
    }

    public static string CreateJsonFromObject(List<PlayerStat> lst)
    {
        return JsonUtility.ToJson(lst.ToArray(), true);
    }
}
