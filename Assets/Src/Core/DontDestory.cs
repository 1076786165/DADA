using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DontDestory:MonoBehaviourSingleton<DontDestory>
{
    protected override void Awake(){
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
