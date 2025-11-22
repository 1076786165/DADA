using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public interface IInitable
{
    bool _isInited { set; get; }

    void MarkInitEnd() => _isInited = true;

    public void Init();

    public bool IsInited()
    {
        return _isInited;
    }

}
