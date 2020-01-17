using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile
{
    static Profile s_instance;
    string profileString=null;
    //string profilePath = Setting.APPLICATION_PATH + "/" + Setting.USER_PROFILE;
    string profilePath = Setting.DATA_PATH + Setting.USER_PROFILE;
    public static Profile instance
    {
        get
        {
            if (s_instance == null)
                s_instance = new Profile();
            return s_instance;
        }
        set
        {
            s_instance = value;
        }
    }
    string LoadProfile(bool isEncode)
    {
        //if (FileSystem.instance.isExits(profilePath))
        //{
        //    profileString = FileSystem.instance.Readfile(profilePath, isEncode);
        //    return profileString;
        //}
        //return null;
        profileString = FileSystem.instance.ReadFromResource(profilePath.Replace(".json",""), isEncode);
        return profileString;
    }
    public string GetProfile(bool isEncode)
    {
        if(string.IsNullOrEmpty(profileString))
        {
            return LoadProfile(isEncode);
        }
        return profileString;
    }
    public bool SaveProfile(string jsonProfile, bool isEncode)
    {
        return FileSystem.instance.WriteToFile(profilePath, jsonProfile, isEncode);
    }
}
