using UnityEngine;

public static class Tools
{
    public static void Log(string msg){
        Debug.Log(msg);
    }

    public static void Log(string msg , object obj){
        Debug.Log($"{obj.GetType().Name}:" + msg);
    }

    public static void LogError(string msg){
        Debug.LogError(msg);
    }
}
