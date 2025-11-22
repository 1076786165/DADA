using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DebugManager : MonoBehaviourSingleton<DebugManager>
{
    public string _debug_str = "";

    public void addDebugStr(string str)
    {
        _debug_str += str;
    }


    public void Log()
    {

    }
}
