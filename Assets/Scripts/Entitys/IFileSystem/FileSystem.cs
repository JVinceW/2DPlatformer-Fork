using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class FileSystem
{
    #region singleton
    static FileSystem m_instance;
    public static FileSystem instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new FileSystem();
            return m_instance;
        }
        set
        {
            m_instance = value;
        }
    }
    #endregion
    public bool WriteToFile(string path, string text, bool isEncode)
    {
        try
        {
            if (isEncode)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(path);
                byte[] bytesToEncode = Encoding.UTF8.GetBytes(text);
                text = Convert.ToBase64String(bytesToEncode);
                bf.Serialize(file, text);
                file.Close();
            }
            else
            {
                StreamWriter write = File.CreateText(path);
                write.Write(text);
                write.Close();
            }
            return true;
        }
        catch
        {
            Logs.LogE("Can't write to file with path : " + path);
            return true;
        }
    }
    public string Readfile(string path, bool isEncode)
    {
        if (isEncode)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(path);
            byte[] ByteToDecode = Convert.FromBase64String((string)bf.Deserialize(file));
            string value = Encoding.UTF8.GetString(ByteToDecode);
            file.Close();
            return value;
        }
        else
        {
            StreamReader write = File.OpenText(path);
            string value = write.ReadToEnd();
            write.Close();
            return value;
        }
    }

    public string ReadFromResource(string path, bool isEncode)
    {
        if (isEncode)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(path);
            byte[] ByteToDecode = Convert.FromBase64String((string)bf.Deserialize(file));
            string value = Encoding.UTF8.GetString(ByteToDecode);
            file.Close();
            return value;
        }
        else
        {
            string value = Resources.Load(path).ToString();
            return value;
        }
    }

    public bool isExits(string path)
    {
        return File.Exists(path);
    }
}
