using UnityEngine;

public class Logs : MonoBehaviour
{
    static bool WriteLogs = true;
    
    static public void LogD(object msg, GameObject context = null)
    {
        Debug.Log(msg, context);
    }
    static public void LogE(object msg, GameObject context = null)
    {
        Debug.LogError(msg, context);
    }
    static public void LogW(object msg, GameObject context = null)
    {
        Debug.LogWarning(msg, context);
    }
}
